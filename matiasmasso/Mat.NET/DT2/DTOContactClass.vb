Public Class DTOContactClass
    Inherits DTOBaseGuid

    Property Nom As DTOLangText
    Property Ord As Integer
    Property SalePoint As Boolean
    Property Raffles As Boolean
    Property DistributionChannel As DTODistributionChannel
    Property Contacts As List(Of DTOContact)

    Public Enum wellknowns
        NotSet
        BotigaPuericultura
        Farmacia
        ParaFarmacia
        MajoristaFarmacies
        Guarderia
        MajoristaGuarderies
        Online
        Staff
        Rep
        Proveidor
    End Enum

    Public Sub New()
        MyBase.New()
        _Contacts = New List(Of DTOContact)
        _Nom = New DTOLangText
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Contacts = New List(Of DTOContact)
        _Nom = New DTOLangText
    End Sub

    Shared Function wellknown(value As DTOContactClass.wellknowns) As DTOContactClass
        Dim retval As DTOContactClass = Nothing
        Select Case value
            Case DTOContactClass.wellknowns.BotigaPuericultura
                retval = New DTOContactClass(New Guid("2C19ABF8-F424-45DF-8690-09F32778A8DB"))
            Case DTOContactClass.wellknowns.Farmacia
                retval = New DTOContactClass(New Guid("61244B59-9E4D-4019-A358-D2932A3E370F"))
            Case DTOContactClass.wellknowns.ParaFarmacia
                retval = New DTOContactClass(New Guid("D0E8740A-3B6F-40D1-85B2-CB68C8497AE3"))
            Case DTOContactClass.wellknowns.MajoristaFarmacies
                retval = New DTOContactClass(New Guid("58597ABE-2D6C-4964-BADE-073F7A993E47"))
            Case DTOContactClass.wellknowns.Guarderia
                retval = New DTOContactClass(New Guid("231CEB14-7036-4BF8-B01F-D3F824A33C86"))
            Case DTOContactClass.wellknowns.MajoristaGuarderies
                retval = New DTOContactClass(New Guid("924D3A0E-B7F9-4F96-BA09-665630C76DEF"))
            Case DTOContactClass.wellknowns.Online
                retval = New DTOContactClass(New Guid("E4796515-F5EE-490A-AAAE-7D15714CDD08"))
            Case DTOContactClass.wellknowns.Staff
                retval = New DTOContactClass(New Guid("7E93ACDE-D6B1-4F4D-AD50-92CB5B151FB9"))
            Case DTOContactClass.wellknowns.Rep
                retval = New DTOContactClass(New Guid("BEA91554-9481-44FE-9C70-EA36844981A6"))
            Case DTOContactClass.wellknowns.Proveidor
                retval = New DTOContactClass(New Guid("D8D627FE-2ED8-4E85-823A-CFAF3A9BD2E7"))
            Case DTOContactClass.wellknowns.NotSet
                retval = Nothing
        End Select
        Return retval
    End Function
End Class
