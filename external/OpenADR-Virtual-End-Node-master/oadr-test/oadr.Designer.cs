﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace oadr_test {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0")]
    internal sealed partial class oadr : global::System.Configuration.ApplicationSettingsBase {
        
        private static oadr defaultInstance = ((oadr)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new oadr())));
        
        public static oadr Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://127.0.0.1:8080/OpenADR2/Simple/2.0b")]
        public string vtnURL {
            get {
                return ((string)(this["vtnURL"]));
            }
            set {
                this["vtnURL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TH_VEN")]
        public string venID {
            get {
                return ((string)(this["venID"]));
            }
            set {
                this["venID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TH_VTN")]
        public string vtnID {
            get {
                return ((string)(this["vtnID"]));
            }
            set {
                this["vtnID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("testing")]
        public string venPassword {
            get {
                return ((string)(this["venPassword"]));
            }
            set {
                this["venPassword"] = value;
            }
        }
    }
}
