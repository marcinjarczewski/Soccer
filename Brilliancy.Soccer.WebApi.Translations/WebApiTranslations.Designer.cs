﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Brilliancy.Soccer.WebApi.Translations {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class WebApiTranslations {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal WebApiTranslations() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Brilliancy.Soccer.WebApi.Translations.WebApiTranslations", typeof(WebApiTranslations).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wysłano zaproszenie..
        /// </summary>
        public static string AuthenticationController_InvitePlayer {
            get {
                return ResourceManager.GetString("AuthenticationController_InvitePlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zostałeś administratorem turnieju..
        /// </summary>
        public static string AuthenticationController_ValidKeyInviteAdmin {
            get {
                return ResourceManager.GetString("AuthenticationController_ValidKeyInviteAdmin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zaproszenie przyjęte..
        /// </summary>
        public static string AuthenticationController_ValidKeyInvitePlayer {
            get {
                return ResourceManager.GetString("AuthenticationController_ValidKeyInvitePlayer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Klucz prawidłowy. Wprowadź nowe hasło. .
        /// </summary>
        public static string AuthenticationController_ValidKeyResetPassword {
            get {
                return ResourceManager.GetString("AuthenticationController_ValidKeyResetPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dane nie zostały uzupełnione prawidłowo..
        /// </summary>
        public static string BaseController_InvalidData {
            get {
                return ResourceManager.GetString("BaseController_InvalidData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} musi mieć conajmniej {2} znaków..
        /// </summary>
        public static string BaseController_StringLength {
            get {
                return ResourceManager.GetString("BaseController_StringLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił niespodziewany błąd..
        /// </summary>
        public static string BaseController_UnexpectedError {
            get {
                return ResourceManager.GetString("BaseController_UnexpectedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Przesłany plik jest pusty..
        /// </summary>
        public static string FileController_FileEmpty {
            get {
                return ResourceManager.GetString("FileController_FileEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił problem podczas przesyłania pliku..
        /// </summary>
        public static string FileController_FileError {
            get {
                return ResourceManager.GetString("FileController_FileError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plik został przesłany..
        /// </summary>
        public static string FileController_FileSuccess {
            get {
                return ResourceManager.GetString("FileController_FileSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Niepoprawny format pliku..
        /// </summary>
        public static string FileController_IncorrectFormat {
            get {
                return ResourceManager.GetString("FileController_IncorrectFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hasła muszą być identyczne.
        /// </summary>
        public static string LoginController_ComparePassword {
            get {
                return ResourceManager.GetString("LoginController_ComparePassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Jeżeli konto istnieje email z przypomnieniem hasła zostanie wysłany..
        /// </summary>
        public static string LoginController_EmailSend {
            get {
                return ResourceManager.GetString("LoginController_EmailSend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nieprawidłowy login lub hasło.
        /// </summary>
        public static string LoginController_InvalidLoginOrPassword {
            get {
                return ResourceManager.GetString("LoginController_InvalidLoginOrPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Język został zmieniony..
        /// </summary>
        public static string LoginController_LanguageChanged {
            get {
                return ResourceManager.GetString("LoginController_LanguageChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login.
        /// </summary>
        public static string LoginController_Login {
            get {
                return ResourceManager.GetString("LoginController_Login", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login może zawierać jedynie znaki a-z A-Z 0-9 _.@.
        /// </summary>
        public static string LoginController_LoginCharacters {
            get {
                return ResourceManager.GetString("LoginController_LoginCharacters", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Logowanie zakończone powodzeniem.
        /// </summary>
        public static string LoginController_LoginSuccessful {
            get {
                return ResourceManager.GetString("LoginController_LoginSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zostałeś wylogowany.
        /// </summary>
        public static string LoginController_LogoutSuccessful {
            get {
                return ResourceManager.GetString("LoginController_LogoutSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hasło.
        /// </summary>
        public static string LoginController_Password {
            get {
                return ResourceManager.GetString("LoginController_Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hasło zostało zmienione..
        /// </summary>
        public static string LoginController_PasswordChanged {
            get {
                return ResourceManager.GetString("LoginController_PasswordChanged", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rejestracja zakończona powodzeniem..
        /// </summary>
        public static string LoginController_RegisterSuccessful {
            get {
                return ResourceManager.GetString("LoginController_RegisterSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dodano nowy mecz.
        /// </summary>
        public static string MatchController_AddSuccess {
            get {
                return ResourceManager.GetString("MatchController_AddSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Edycja meczu.
        /// </summary>
        public static string MatchController_EditTitle {
            get {
                return ResourceManager.GetString("MatchController_EditTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dane o zawodnikach zostały zapisane..
        /// </summary>
        public static string PlayerController_Edit {
            get {
                return ResourceManager.GetString("PlayerController_Edit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Administrator został dodany..
        /// </summary>
        public static string TournamentController_AdminAdded {
            get {
                return ResourceManager.GetString("TournamentController_AdminAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Administrator został usunięty..
        /// </summary>
        public static string TournamentController_AdminRemoved {
            get {
                return ResourceManager.GetString("TournamentController_AdminRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Turniej został utworzony..
        /// </summary>
        public static string TournamentController_Created {
            get {
                return ResourceManager.GetString("TournamentController_Created", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Utwórz turniej.
        /// </summary>
        public static string TournamentController_CreateTitle {
            get {
                return ResourceManager.GetString("TournamentController_CreateTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Szczegóły turnieju.
        /// </summary>
        public static string TournamentController_DetailsTitle {
            get {
                return ResourceManager.GetString("TournamentController_DetailsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Zmiany zostały zapisane..
        /// </summary>
        public static string TournamentController_Edited {
            get {
                return ResourceManager.GetString("TournamentController_Edited", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Edytuj turniej.
        /// </summary>
        public static string TournamentController_EditTitle {
            get {
                return ResourceManager.GetString("TournamentController_EditTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Turnieje.
        /// </summary>
        public static string TournamentController_IndexTitle {
            get {
                return ResourceManager.GetString("TournamentController_IndexTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dane turnieju nie zostały uzupełnione poprawnie..
        /// </summary>
        public static string TournamentController_InvalidTournamentData {
            get {
                return ResourceManager.GetString("TournamentController_InvalidTournamentData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Odzyskiwanie dostępu.
        /// </summary>
        public static string TournamentController_LostPasswordTitle {
            get {
                return ResourceManager.GetString("TournamentController_LostPasswordTitle", resourceCulture);
            }
        }
    }
}
