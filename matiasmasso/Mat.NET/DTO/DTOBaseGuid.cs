namespace DTO
{
    using System;
    using System.Collections.Generic;

    public class DTOBaseGuid
    {
        private Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; IsNew = false; }
        }
        public bool IsNew { get; set; }
        public bool IsLoaded { get; set; }

        public DTOBaseGuid() : base()
        {
            _guid = System.Guid.NewGuid();
            IsNew = true;
        }

        public DTOBaseGuid(Guid oGuid) : base()
        {
            Guid = oGuid;
        }

        public static T Opcional<T>(Guid oGuid) where T : DTOBaseGuid
        {
            T retval = null;
            if (oGuid != Guid.Empty)
            {
                retval = (T)Activator.CreateInstance(typeof(T), oGuid);
            }
            return retval;
        }


        public bool UnEquals(DTOBaseGuid oCandidate)
        {
            bool retval = !Equals(oCandidate);
            return retval;
        }

        public bool Equals(DTOBaseGuid oCandidate)
        {
            bool retval = false;
            if (oCandidate != null)
                retval = Guid.Equals(oCandidate.Guid);
            return retval;
        }

        public bool notEquals(DTOBaseGuid oCandidate)
        {
            bool retval = !Equals(oCandidate);
            return retval;
        }

        public bool notEquals(Guid oCandidateGuid)
        {
            bool retval = !Guid.Equals(oCandidateGuid);
            return retval;
        }

        public void renewGuid()
        {
            // per clonar objectes
            Guid = System.Guid.NewGuid();
            IsNew = true;
        }

        public string GuionLessGuid()
        {
            string retval = Guid.ToString("N");
            return retval;
        }


        public static bool CopyPropertyValues<T>(T from, T to, List<Exception> exs)
        {
            bool retval = false;
            if (from != null)
            {

                //System.Reflection.PropertyInfo oProperty;
                try
                {
                    Type oType = typeof(T);
                    var oProperties = oType.GetProperties();

                    foreach (System.Reflection.PropertyInfo oProperty in oProperties)
                    {
                        try
                        {
                            if (oProperty.CanWrite)
                            {
                                try
                                {
                                    var propertyValue = oProperty.GetValue(from);
                                    oProperty.SetValue(to, propertyValue);
                                }
                                catch (Exception ex)
                                {
                                    exs.Add(ex);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            exs.Add(ex);
                        }
                    }
                    retval = true;
                }
                catch (Exception ex)
                {
                    exs.Add(ex);
                }
            }
            return retval;
        }

        // Public Function Trimmed(Of T As New)() As T
        // Dim retval = Activator.CreateInstance(GetType(T), _Guid)
        // Return retval
        // End Function

        public DTOBaseGuid Trimmed()
        {
            List<Exception> exs = new List<Exception>();
            Type oType = this.GetType();
            var retval = Activator.CreateInstance(this.GetType());
            try
            {
                var oProperties = oType.GetProperties();
                foreach (var oProperty in oProperties)
                {
                    if (typeof(DTOBaseGuid).IsAssignableFrom(oProperty.PropertyType))
                    {
                        try
                        {
                            DTOBaseGuid oBaseGuid = (DTOBaseGuid)oProperty.GetValue(this);
                            if (oBaseGuid != null)
                            {
                                var oTrimmedPropertyValue = Activator.CreateInstance(oProperty.PropertyType, oBaseGuid.Guid);
                                oProperty.SetValue(retval, oTrimmedPropertyValue);
                            }
                        }
                        catch (Exception ex)
                        {
                            oProperty.SetValue(retval, oProperty.GetValue(this));
                            exs.Add(ex);
                        }
                    }
                    else
                        oProperty.SetValue(retval, oProperty.GetValue(this));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                exs.Add(ex);
            }
            return (DTOBaseGuid)retval;
        }

        public static T Trim<T>(T oSource, List<Exception> exs)
        {
            Type oType = oSource.GetType();
            var retval = Activator.CreateInstance(oSource.GetType());
            try
            {
                var oProperties = oType.GetProperties();
                foreach (var oProperty in oProperties)
                {
                    if (typeof(DTOBaseGuid).IsAssignableFrom(oProperty.PropertyType))
                    {
                        try
                        {
                            DTOBaseGuid oBaseGuid = (DTOBaseGuid)oProperty.GetValue(oSource);
                            if (oBaseGuid != null)
                            {
                                var oTrimmedPropertyValue = Activator.CreateInstance(oProperty.PropertyType, oBaseGuid.Guid);
                                oProperty.SetValue(retval, oTrimmedPropertyValue);
                            }
                        }
                        catch (Exception ex)
                        {
                            oProperty.SetValue(retval, oProperty.GetValue(oSource));
                            exs.Add(ex);
                        }
                    }
                    else if (oProperty.PropertyType.IsPrimitive || oProperty.PropertyType == typeof(System.Guid) || oProperty.PropertyType == typeof(DateTime) || oProperty.PropertyType == typeof(decimal) || oProperty.PropertyType == typeof(string) || oProperty.PropertyType == typeof(int) || oProperty.PropertyType.IsEnum)
                    {
                        var oRawValue = oProperty.GetValue(oSource);
                        oProperty.SetValue(retval, oRawValue);
                    }
                    else
                    {
                        var oRawValue = oProperty.GetValue(oSource);
                        if (oRawValue != null)
                        {
                            var oTrimmedValue = DTOBaseGuid.Trim(oRawValue, exs);
                            oProperty.SetValue(retval, oTrimmedValue);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exs.Add(ex);
            }
            return (T)retval;
        }

        public string urlSegment(string segment)
        {
            var retval = string.Format("/{0}/{1}", segment, Guid.ToString());
            return retval;
        }

        //public string Serialized()
        //{
        //    var serializer = new JavaScriptSerializer();
        //    string retval = serializer.Serialize(this);
        //    return retval;
        //}
        //public static string Serialized(Object src)
        //{
        //    var serializer = new JavaScriptSerializer();
        //    string retval = serializer.Serialize(src);
        //    return retval;
        //}
    }

}
