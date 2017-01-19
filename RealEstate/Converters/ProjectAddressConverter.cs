using log4net;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RealEstate
{
    public class ProjectAddressConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(ProjectAddressConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;

            try
            {
                Project project = value as Project;

                if (project != null)
                {
                    returnedValue = $"{project.City?.Name}, {project.Street} {project.HouseNumber?.ToString()}";
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            return returnedValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
