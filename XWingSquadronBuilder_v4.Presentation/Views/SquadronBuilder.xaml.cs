using Template10.Services.NavigationService;
using Windows.UI.Xaml;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XWingSquadronBuilder_v4.Interfaces;
using XWingSquadronBuilder_v4.Presentation.UserControls;
using XWingSquadronBuilder_v4.Presentation.ViewModels;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages;
using XWingSquadronBuilder_v4.Presentation.ViewModels.Pages.Interfaces;
using static Template10.Common.BootStrapper;
using XWingSquadronBuilder_v4.Presentation.Services.Printing;
using Windows.UI.Xaml.Printing;
using Windows.Graphics.Printing;
using Windows.UI.Xaml.Input;
using XWingSquadronBuilder_v4.Presentation.UserControls.PrintControls;
using System.Linq;
using System.Collections.Generic;
using XWingSquadronBuilder_v4.Presentation.ViewModels.XWingModels;
using XWingSquadronBuilder_v4.BusinessLogic.Models.NullModels;
using Windows.Foundation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace XWingSquadronBuilder_v4.Presentation.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SquadronBuilder : Page
    {
        public ISquadronBuilderViewModel ViewModel
        {
            get { return (ISquadronBuilderViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ISquadronBuilderViewModel), typeof(SquadronBuilder), new PropertyMetadata(new SquadronBuilderViewModel()));

        private PrintDocument printDocument;
        private IPrintDocumentSource printDocumentSource;
        private List<UIElement> printPreviewPages;

        public SquadronBuilder()
        {
            ViewModel = new SquadronBuilderViewModel();
            DataContext = ViewModel;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RegisterForPrinting();
            // Tell the user how to print
            //MainPage.Current.NotifyUser("Print contract registered with customization, use the Print button to print.", NotifyType.StatusMessage);
            base.OnNavigatedTo(e);
        }

        private void RegisterForPrinting()
        {
            printDocument = new PrintDocument();
            printDocumentSource = printDocument.DocumentSource;
            printDocument.Paginate += CreatePrintPreviewPages;
            printDocument.GetPreviewPage += GetPrintPreviewPage;
            printDocument.AddPages += AddPrintPages;

            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested += PrintTaskRequested;
        }

        private void PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs e)
        {
            PrintTask printTask = null;
            printTask = e.Request.CreatePrintTask("Squadron Builder", sourceRequested =>
            {
                // Print Task event handler is invoked when the print job is completed.
                printTask.Completed += async (s, args) =>
                {
                    // Notify the user when the print operation fails.
                    if (args.Completion == PrintTaskCompletion.Failed)
                    {
                        await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                        {
                            ContentDialog noPrintingDialog = new ContentDialog()
                            {
                                Title = "Printing error",
                                Content = "\nSorry, printing can' t proceed at this time.",
                                PrimaryButtonText = "OK"
                            };
                            await noPrintingDialog.ShowAsync();

                        });
                    }
                };

                sourceRequested.SetSource(printDocumentSource);
            });
        }

        private void AddPrintPages(object sender, AddPagesEventArgs e)
        {            
            // Loop over all of the preview pages and add each one to  add each page to be printied
            for (int i = 0; i < printPreviewPages.Count; i++)
            {
                // We should have all pages ready at this point...
                printDocument.AddPage(printPreviewPages[i]);
            }

            PrintDocument printDoc = (PrintDocument)sender;

            // Indicate that all of the print pages have been provided
            printDoc.AddPagesComplete();
        }

        private void GetPrintPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            PrintDocument printDoc = (PrintDocument)sender;
            printDoc.SetPreviewPage(e.PageNumber, printPreviewPages[e.PageNumber - 1]);
        }

        private void CreatePrintPreviewPages(object sender, PaginateEventArgs e)
        {
            printPreviewPages = new List<UIElement>();
            PrintCanvas.Children.Clear();
            var printOptions = e.PrintTaskOptions;
            var pageDescription = printOptions.GetPageDescription(0);
            PrintDocument printDoc = (PrintDocument)sender;



            var printPage = CreateNewPrintPage(pageDescription.PageSize);

            SquadronPrintHeader printHeader = new SquadronPrintHeader()
            {
                ViewModel = this.ViewModel.SquadronViewModel,
                Margin = new Thickness(10)
            };
            printPage.AddContent(printHeader);
            var Pilots = ViewModel.SquadronViewModel.Pilots;

            for(int i = 0; i < Pilots.Count(); i++)
            {
                
                PilotPrintControl pilotPrint = new PilotPrintControl()
                {
                    ViewModel = Pilots[i],
                    Margin = new Thickness(10)
                };
                printPage.AddContent(pilotPrint);
                PrintCanvas.InvalidateMeasure();
                PrintCanvas.UpdateLayout();

                
                if (printPage.ActualHeight >= pageDescription.PageSize.Height)
                {
                    printPage.RemoveLastContentItem();
                    printPreviewPages.Add(printPage);
                    printPage = CreateNewPrintPage(pageDescription.PageSize);
                    i--;
                }
            }
            printPreviewPages.Add(printPage);
            PrintCanvas.Children.Clear();
            

            foreach (var item in printPreviewPages)
            {
                PrintCanvas.Children.Add(item);
            }
            PrintCanvas.InvalidateMeasure();
            PrintCanvas.UpdateLayout();
            // Report the number of preview pages created
            printDoc.SetPreviewPageCount(printPreviewPages.Count, PreviewPageCountType.Intermediate);
        }

        private SquadronPrintPage CreateNewPrintPage(Size pageSize)
        {
            SquadronPrintPage printPage = new SquadronPrintPage();
            printPage.MinWidth = pageSize.Width;
            printPage.MaxWidth = pageSize.Width;
            //printPage.Padding = new Thickness(pageSize.Width * .15, pageSize.Height * .15, pageSize.Width * .15, pageSize.Height * .15);
            PrintCanvas.Children.Clear();
            PrintCanvas.Children.Add(printPage);
            return printPage;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (printDocument == null)
            {
                return;
            }

            printDocument.Paginate -= CreatePrintPreviewPages;
            printDocument.GetPreviewPage -= GetPrintPreviewPage;
            printDocument.AddPages -= AddPrintPages;

            // Remove the handler for printing initialization.
            PrintManager printMan = PrintManager.GetForCurrentView();
            printMan.PrintTaskRequested -= PrintTaskRequested;

            PrintCanvas.Children.Clear();
            base.OnNavigatedFrom(e);
        }

       // private PrintHelper printHelper { get; set; }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveSquadronDialog dialog = new SaveSquadronDialog();
            await dialog.ShowAsync();
        }

        private async void btnPrint_Tapped(object sender, TappedRoutedEventArgs e)
        {            
            await PrintManager.ShowPrintUIAsync();
        }


    }
}
