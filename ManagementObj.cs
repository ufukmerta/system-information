namespace WFASystemInformation
{
    internal class ManagementObj
    {
        internal string mgmtObject;
        internal bool mgmtChecked;
        public ManagementObj(string managementObject, bool managementChecked=true)
        {
            this.mgmtObject = managementObject;
            this.mgmtChecked = managementChecked;
        }        
    }
}
