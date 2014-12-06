using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SCI.BL.Entities;

namespace SCI.App.Views.TemplateSelectors
{
    public class WallEntryTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextEntryDataTemplate { get; set; }
        public DataTemplate ImageEntryDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            if (item.GetType() == typeof (ImageWallEntry))
            {
                return ImageEntryDataTemplate;
            }

            if (item.GetType() == typeof (TextWallEntry))
            {
                return TextEntryDataTemplate;
            }

            return null;
        }
    }
}
