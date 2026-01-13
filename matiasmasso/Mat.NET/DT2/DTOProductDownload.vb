Public Class DTOProductDownload
    Inherits DTOBaseGuid

    Property Target As DTOBaseGuid
    Property Targets As List(Of DTOBaseGuidCodNom)
    Property DocFile As DTODocFile
    Property Src As Srcs
    Public Property Lang As DTOLang
    Property PublicarAlConsumidor As Boolean
    Property PublicarAlDistribuidor As Boolean

    Property Obsoleto As Boolean

    Property FileCount As Integer

    'ojo afectan a urls
    Public Enum Srcs
        NotSet
        Instrucciones
        Catalogos
        Compatibilidad
        Despiece
        Imatge_Alta_Resolucio
        Certificat_Homologacio
        Publicacions
        Seguro
        Justificant
        Documentacio
        Proforma
    End Enum

    Public Enum TargetCods
        Product
    End Enum

    Public Sub New()
        MyBase.New()
        _Targets = New List(Of DTOBaseGuidCodNom)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Targets = New List(Of DTOBaseGuidCodNom)
    End Sub

    Shared Function Factory(oTarget As DTOBaseGuid, exs As List(Of Exception)) As DTOProductDownload
        Dim retval As New DTOProductDownload
        retval.AddTarget(oTarget, exs)
        Return retval
    End Function

    Public Function AddTarget(value As DTOBaseGuid, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim oTarget As DTOBaseGuidCodNom = Nothing
        If TypeOf value Is DTOVehicle Then
            oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.ProductBrand, String.Format("Vehicle matricula {0}", CType(value, DTOVehicle).Matricula))
        ElseIf TypeOf value Is DTOLiniaTelefon Then
            oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.LiniaTelefon, String.Format("telefon {0}", CType(value, DTOLiniaTelefon).Num))
        ElseIf TypeOf value Is DTOProductBrand Then
            oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.ProductBrand, CType(value, DTOProductBrand).Nom)
        ElseIf TypeOf value Is DTOProductCategory Then
            oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.ProductBrand, CType(value, DTOProductCategory).Nom)
        ElseIf TypeOf value Is DTOProductSku Then
            oTarget = DTOBaseGuidCodNom.Factory(value.Guid, DTOBaseGuidCodNom.Cods.ProductBrand, DTOProductSku.FullNom(value))
        Else
            exs.Add(New Exception("target no permés"))
        End If
        If exs.Count = 0 Then
            _Targets.Add(oTarget)
            retval = True
        End If
        Return retval
    End Function

    Shared Function Nom(oDownload As DTOProductDownload) As String
        Dim retval As String = ""
        If TypeOf oDownload.Target Is DTOProduct Then
            retval = DTOProduct.GetNom(oDownload.Target)
        ElseIf TypeOf oDownload.Target Is DTOVehicle Then
            Dim oVehicle As DTOVehicle = oDownload.Target
            retval = oVehicle.MarcaModelYMatricula
        End If
        Return retval
    End Function
End Class
