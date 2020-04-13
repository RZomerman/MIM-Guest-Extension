using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MetadirectoryServices;

namespace AAD_B2B_Guest_MIM_Extensions
{
    public class MaExtension : IMASynchronization
    {
        public DeprovisionAction Deprovision(CSEntry csentry)
        {
            throw new NotImplementedException();
        }

        public bool FilterForDisconnection(CSEntry csentry)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
        }

        public void MapAttributesForExport(string FlowRuleName, MVEntry mventry, CSEntry csentry)
        {
            throw new NotImplementedException();
        }

        public void MapAttributesForImport(string FlowRuleName, CSEntry csentry, MVEntry mventry)
        {
            switch(FlowRuleName.ToLower())
            {
                case "cd.user:id->mv.person:accountname":
                    if (csentry["id"].IsPresent && csentry["id"].StringValue.Length > 20)
                        mventry["accountName"].StringValue = csentry["id"].StringValue.Substring(0, 19);
                    else
                        mventry["accounName"].StringValue = csentry["id"].StringValue;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void MapAttributesForJoin(string FlowRuleName, CSEntry csentry, ref ValueCollection values)
        {
            throw new NotImplementedException();
        }

        public bool ResolveJoinSearch(string joinCriteriaName, CSEntry csentry, MVEntry[] rgmventry, out int imventry, ref string MVObjectType)
        {
            throw new NotImplementedException();
        }

        public bool ShouldProjectToMV(CSEntry csentry, out string MVObjectType)
        {
            throw new NotImplementedException();
        }

        public void Terminate()
        {
        }
    }
}
