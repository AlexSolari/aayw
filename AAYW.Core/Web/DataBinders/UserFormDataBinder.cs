using AAYW.Core.Dependecies;
using AAYW.Core.Models.View.UserForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AAYW.Core.Web.DataBinders
{
    public class UserFormDataBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            var result = Resolver.GetInstance<UserFormDesignModel>();

            dynamic target = result;

            var resultType = result.GetType();
            var regex = new Regex(@"(Fields)\[([0-9]+)\]\.(\w+)");

            foreach (string field in request.Form)
            {
                var extractedValue = request.Form.Get(field);
                PropertyInfo prop;
                if (regex.IsMatch(field))
                {
                    var groups = regex.Match(field).Groups;
                    var index = int.Parse(groups[2].Value);
                    if (groups[1].Value.Equals("Fields") && index >= result.FormFields.Count)
                    {
                        while(result.FormFields.Count <= index)
                        {
                            result.FormFields.Add(new UserFormField());
                        }
                    }
                    prop = result.FormFields[index].GetType().GetProperty(groups[3].Value);
                    target = result.FormFields[index];
                }
                else
                {
                    prop = resultType.GetProperty(field);
                }

                if (prop == null)
                {
                    continue;
                }
                dynamic validValue;
                if (prop.PropertyType.IsEnum)
                {
                    validValue = Enum.Parse(prop.PropertyType, extractedValue);
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    validValue = extractedValue.Equals("on");
                }
                else if (prop.PropertyType == typeof(int) || 
                    prop.PropertyType == typeof(float) || 
                    prop.PropertyType == typeof(double) || 
                    prop.PropertyType == typeof(decimal))
                {
                    validValue = Convert.ChangeType(extractedValue, prop.PropertyType);
                }
                else
                {
                    validValue = extractedValue;
                }
                prop.SetValue(target, validValue);
            }

            return result;
        }
    }
}
