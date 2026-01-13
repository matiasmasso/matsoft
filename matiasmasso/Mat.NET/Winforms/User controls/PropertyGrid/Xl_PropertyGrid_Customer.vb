Public Class Xl_PropertyGrid_Customer
    Inherits Xl_PropertyGrid

    Private _Customer As DTOCustomer

    Public Shadows Sub Load(oCustomer As DTOCustomer)
        _Customer = oCustomer
        With oCustomer
            MyBase.AddGroup("Contacte")
            MyBase.AddItem("Rao Social", "Nom", DTOPropertyGridItem.Editors.TextBox, .Nom)
            MyBase.AddItem("Nom Comercial", "NomComercial", DTOPropertyGridItem.Editors.TextBox, .NomComercial)
            MyBase.AddItem("NIF", "NIF", DTOPropertyGridItem.Editors.TextBox, .PrimaryNifValue())
            MyBase.AddItem("Adreça", "Address", DTOPropertyGridItem.Editors.Lookup, .Address)
            MyBase.AddItem("Llengua", "Lang", DTOPropertyGridItem.Editors.Lang, .Lang)
            MyBase.AddItem("Obsolet", "Obsoleto", DTOPropertyGridItem.Editors.CheckBox, .Obsoleto)
        End With
    End Sub

    Public ReadOnly Property Value As DTOCustomer
        Get
            With _Customer
                .Nom = MyBase.PropertyValue("Nom")
                .NomComercial = MyBase.PropertyValue("NomComercial")
                .Nif = MyBase.PropertyValue("NIF")
                .Lang = MyBase.PropertyValue("Lang")
                .Obsoleto = MyBase.PropertyValue("Obsoleto")
            End With
            Return _Customer
        End Get
    End Property

End Class
