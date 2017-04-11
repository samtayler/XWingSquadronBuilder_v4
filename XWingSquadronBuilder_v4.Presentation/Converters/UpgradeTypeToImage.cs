using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


namespace XWingSquadronBuilder_v4.Presentation.Converters
{
    //public class UpgradeTypeToImage : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, string language)
    //    {
    //        if (value is UpgradeType)
    //        {
    //            var imageUri = new Uri(UpgradeCardFunctions.InferImageFromModType((UpgradeType)value), UriKind.RelativeOrAbsolute);
    //            BitmapImage imageBitmap = new BitmapImage(imageUri);               
    //            return imageBitmap;
    //        }
    //        if (value is List<UpgradeType>)
    //        {
    //            List<string> list = new List<string>();
    //            foreach (var item in (List<UpgradeType>)value)
    //            {
    //                list.Add(UpgradeCardFunctions.InferImageFromModType(item));
    //            }

    //            return list;
    //        }
    //        return "";
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, string language)
    //    {
    //        return "";
    //    }
    //}


    //public class UpgradeCardFunctions
    //{
    //    public static string InferImageFromModType(IUpgradeType modtype)
    //    {
    //        switch (modtype)
    //        {
    //            case (UpgradeType.Astromech):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Astromech.png";
    //                }
    //            case (UpgradeType.Bomb):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Bomb.png";
    //                }
    //            case (UpgradeType.Cannon):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Cannon.png";
    //                }
    //            case (UpgradeType.Cargo):
    //                {
    //                    return "";
    //                }
    //            case (UpgradeType.Crew):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Crew.png";
    //                }
    //            case (UpgradeType.ElitePilotTalent):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/ElitePilotTalent.png";
    //                }
    //            case (UpgradeType.Hardpoint):
    //                {
    //                    return "";
    //                }
    //            case (UpgradeType.Illicit):
    //                {
    //                    return "";
    //                }
    //            case (UpgradeType.Missile):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Missile.png";
    //                }
    //            case (UpgradeType.Modification):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Modification.png";
    //                }
    //            case (UpgradeType.SalvagedAstromech):
    //                {
    //                    return "";
    //                }
    //            case (UpgradeType.SystemUpgrade):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/SystemUpgrade.png";
    //                }
    //            case (UpgradeType.Team):
    //                {
    //                    return "";
    //                }
    //            case (UpgradeType.Tech):
    //                {
    //                    return "";
    //                }
    //            case (UpgradeType.Title):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Title.png";
    //                }
    //            case (UpgradeType.Torpedo):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Torpedo.png";
    //                }
    //            case (UpgradeType.Turret):
    //                {
    //                    return "ms-appx:/Assets/Logos/basic icons/Turret.png";
    //                }
    //            default:
    //                return "";
    //        }

     //   }
   // }
}
