﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheXDS.Ganymede.Resources.Strings {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ProteusDemoView {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ProteusDemoView() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TheXDS.Ganymede.Resources.Strings.ProteusDemoView", typeof(ProteusDemoView).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
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
        ///   Busca una cadena traducida similar a This page will guide you through Proteus, the dynamic UI generation toolkit.
        ///
        ///Born as an idea back in 2018, Proteus is designed to provide a set of tools to help you quickly create a data-bound application, with all the generic CRUD UI and data retrieval-storage being handled behind the scenes automatically. For this end, Proteus will generate ViewModels and Views to create, read, update and delete entities of a specified type.
        ///
        ///The only things Proteus needs to generate these elements is, of course, the [resto de la cadena truncado]&quot;;.
        /// </summary>
        public static string HelpText {
            get {
                return ResourceManager.GetString("HelpText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Manage comments.
        /// </summary>
        public static string ManageComments {
            get {
                return ResourceManager.GetString("ManageComments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Manage posts.
        /// </summary>
        public static string ManagePosts {
            get {
                return ResourceManager.GetString("ManagePosts", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Manage users.
        /// </summary>
        public static string ManageUsers {
            get {
                return ResourceManager.GetString("ManageUsers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Proteus demo page.
        /// </summary>
        public static string Title {
            get {
                return ResourceManager.GetString("Title", resourceCulture);
            }
        }
    }
}
