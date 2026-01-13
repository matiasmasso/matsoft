Public Class DTOSpv
    Inherits DTOBaseGuid

    Public Shadows Property Guid As Guid
        'obligatori per serializer
        Get
            Return MyBase.Guid
        End Get
        Set(value As Guid)
            MyBase.Guid = value
        End Set
    End Property

    Property Emp As DTOEmp
    Property Id As Integer
    Property FchAvis As Date
    Property FchRead As Date
    Property Customer As DTOCustomer
    Property SolicitaGarantia As Boolean
    Property Garantia As Boolean
    Property Contacto As String
    Property sRef As String
    Property SpvIn As DTOSpvIn
    Property Product As Object
    Property SerialNumber As String
    Property ObsClient As String
    Property ObsTecnic As String
    Property LabelEmailedTo As String
    Property Nom As String
    Property Address As DTOAddress
    Property Tel As String

    Property ValJob As DTOAmt
    Property ValMaterial As DTOAmt
    Property ValEmbalatje As DTOAmt
    Property ValPorts As DTOAmt
    Property Delivery As DTODelivery
    Property Incidencia As DTOIncidencia

    Property UsrRegister As DTOUser
    Property UsrTecnic As DTOUser

    Property UsrOutOfSpvIn As DTOUser
    Property FchOutOfSpvIn As Date
    Property ObsOutOfSpvIn As String
    Property UsrOutOfSpvOut As DTOUser
    Property FchOutOfSpvOut As Date
    Property ObsOutOfSpvOut As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oUsrRegister As DTOUser) As DTOSpv
        Dim retval As New DTOSpv
        With retval
            .Emp = oUsrRegister.Emp
            .UsrRegister = oUsrRegister
            .ValJob = DTOAmt.Empty()
            .FchAvis = Today
        End With
        Return retval
    End Function

    Public Function GetProduct() As DTOProduct
        Dim retval As DTOProduct = Nothing
        If _Product IsNot Nothing Then
            If _Product.GetType().IsSubclassOf(GetType(DTOProduct)) Then
                retval = _Product
            Else
                Dim oProduct As DTOProduct = _Product.toobject(Of DTOProduct)
                Select Case oProduct.SourceCod
                    Case DTOProduct.SourceCods.SKU
                        retval = _Product.toobject(Of DTOProductSku)
                    Case DTOProduct.SourceCods.Category
                        retval = _Product.toobject(Of DTOProductCategory)
                    Case DTOProduct.SourceCods.Brand
                        retval = _Product.toobject(Of DTOProductBrand)
                End Select
            End If
        End If
        Return retval
    End Function

    Public Function Lines(oLang As DTOLang) As List(Of String)
        Dim retval As New List(Of String)

        Dim Str As String = oLang.tradueix("Reparación num.", "Reparació num.", "Service Job num.")
        Str = Str & _Id & " "
        If _Garantia Then
            Str = Str & oLang.tradueix("en garantía", "en garantía", "under warranty")
        Else
            Str = Str & oLang.tradueix("con cargo", "amb carrec", "")
        End If
        retval.Add(Str)

        Str = oLang.tradueix("Solicitada", "Sol·licitada", "Requested")
        If _Contacto > "" Then
            Str = Str & oLang.tradueix(" por ", " per ", " by ") & _Contacto
        End If
        Str = Str & oLang.tradueix(" en fecha ", " en data ", " on ") & _FchAvis.ToShortDateString
        retval.Add(Str)

        Str = oLang.tradueix("Recibida el ", "Rebuda el ", "Received on ") & _SpvIn.Fch.ToShortDateString
        If _sRef > "" Then
            Str = Str & " (s/ref.: " & _sRef & ")"
        End If
        retval.Add(Str)

        Str = oLang.tradueix("Producto: ", "Producte: ", "Product: ") & DTOProduct.GetNom( _Product)
        retval.Add(Str)

        If _SerialNumber > "" Then
            Str = oLang.tradueix("Número de serie: ", "Número de serie: ", "Serial number: ") & _SerialNumber
            retval.Add(Str)
        End If

        retval.Add("")

        Dim sObsTecnic As String() = _ObsTecnic.Split(vbLf)
        For Each Str In sObsTecnic
            retval.Add(Str.Trim)
        Next

        retval.Add("")

        Return retval
    End Function

    Shared Function TextRegistre(oSpv As DTOSpv, ByVal oLang As DTOLang) As String
        Dim sUsrRegisterText As String = ""
        Dim ESP As String = "registrado"
        Dim CAT As String = "registrat"
        Dim ENG As String = "recorder"

        If oSpv.UsrRegister IsNot Nothing Then
            Dim sLogin As String = DTOUser.NicknameOrElse(oSpv.UsrRegister)
            ESP = ESP & " por " & sLogin
            CAT = CAT & " per " & sLogin
            ENG = ENG & " by " & sLogin
        End If

        Dim sFch As String = oSpv.FchAvis.ToShortDateString
        ESP = ESP & " el " & sFch
        CAT = CAT & " el " & sFch
        ENG = ENG & " on " & sFch

        Dim retVal As String = oLang.tradueix(ESP, CAT, ENG)
        Return retVal
    End Function

    Shared Function textOutSpvIn(oSpv As DTOSpv) As String
        Dim s As String = "retirat de pendents de entrar per " & DTOUser.NicknameOrElse(oSpv.UsrOutOfSpvIn)
        s = s & " el " & oSpv.FchOutOfSpvIn.ToShortDateString
        If oSpv.ObsOutOfSpvIn > "" Then
            s = s & ": " & oSpv.ObsOutOfSpvIn
        End If
        Return s
    End Function

    Shared Function textOutSpvOut(oSpv As DTOSpv) As String
        Dim s As String = "retirat de pendents de sortir per " & DTOUser.NicknameOrElse(oSpv.UsrOutOfSpvOut)
        s = s & " el " & oSpv.FchOutOfSpvOut.ToShortDateString
        If oSpv.ObsOutOfSpvOut > "" Then
            s = s & ": " & oSpv.ObsOutOfSpvOut
        End If
        Return s
    End Function

End Class
