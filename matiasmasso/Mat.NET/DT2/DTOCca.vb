Public Class DTOCca
    Inherits DTOBaseGuid

    Property exercici As DTOExercici
    Property id As Integer
    Property concept As String
    Property fch As Date
    Property ccd As CcdEnum
    Property cdn As Integer
    Property bookFra As DTOBookFra
    Property projecte As DTOProjecte

    Property usrLog As DTOUsrLog

    Property docFile As DTODocFile
    Property ref As DTOBaseGuid
    Property items As List(Of DTOCcb)

    Property pnds As List(Of DTOPnd)
    Property eur As DTOAmt


    Public Enum CcdEnum
        NotSet = 0
        AperturaExercisi = 1
        MigracioPlaComptable = 2
        [Unknown] = 3
        AlbaraBotiga = 5
        FacturaNostre = 10
        ReclamacioEfecte = 11
        Venciment = 12
        Reemborsament = 14
        CobramentACompte = 15
        XecNostre = 16
        RemesaEfectes = 17
        VisaCobros = 18
        RemesaXecs = 19
        Impagat = 20
        Cobro = 21
        IngresXecs = 22
        XecRebut = 23
        DespesesRemesa = 25
        FacturaProveidor = 30
        Transit = 30004
        FacturaInsercionsPublicitaries = 31
        TransferNorma34 = 34
        Manual = 49
        Pagament = 50
        DipositIrrevocableCompra = 56101
        Nomina = 60
        SegSocTc1 = 61
        RepComisions = 62
        IAE = 63101
        IBI = 63102
        InteresosNostreFavor = 70
        IRPF = 80
        IVA = 81
        InventariMensual = 87
        InventariMensualDesvaloritzacio = 88
        InmovilitzatAlta = 89
        Amortitzacions = 90
        InmovilitzatBaixa = 91
        TancamentComptes = 96
        ImpostSocietats = 97
        TancamentExplotacio = 98
        TancamentBalanç = 99
    End Enum

    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOCcb)
        _UsrLog = New DTOUsrLog
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTOCcb)
        _UsrLog = New DTOUsrLog
    End Sub

    Shared Function Factory(DtFch As Date, oUser As DTOUser, oCcd As DTOCca.CcdEnum, Optional iCdn As Integer = 0) As DTOCca
        Dim retval As New DTOCca
        With retval
            .Exercici = New DTOExercici(oUser.Emp, DtFch.Year)
            .Fch = DtFch
            .Ccd = oCcd
            .Cdn = iCdn
            .Items = New List(Of DTOCcb)
            .UsrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function

    Public Function formattedId() As String
        Dim retval As String = String.Format("{0:0000}{1:0000}", _Fch.Year, _Id)
        Return retval
    End Function

    Shared Function FullNom(oCca As DTOCca) As String
        Dim retval As String = ""
        If oCca IsNot Nothing Then
            If oCca.Fch = Nothing Then
                retval = oCca.Concept
            Else
                retval = String.Format("{0:dd/MM/yy} - {1}", oCca.Fch, oCca.Concept)
            End If
        End If
        Return retval
    End Function

    Public Function AddDebit(oAmt As DTOAmt, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional oPnd As DTOPnd = Nothing) As DTOCcb
        Dim retval As DTOCcb = AddCcb(oAmt, oCta, oContact, DTOCcb.DhEnum.Debe, oPnd)
        Return retval
    End Function

    Public Function AddCredit(oAmt As DTOAmt, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional oPnd As DTOPnd = Nothing) As DTOCcb
        Dim retval As DTOCcb = AddCcb(oAmt, oCta, oContact, DTOCcb.DhEnum.Haber, oPnd)
        Return retval
    End Function


    Public Function AddSaldo(oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional oPnd As DTOPnd = Nothing) As DTOCcb
        Dim oDebit = DTOAmt.factory
        Dim oCredit = DTOAmt.factory
        Dim oSaldo = DTOAmt.factory

        For Each item As DTOCcb In _Items
            Select Case item.Dh
                Case DTOCcb.DhEnum.Debe
                    oDebit.Add(item.Amt)
                Case DTOCcb.DhEnum.Haber
                    oCredit.Add(item.Amt)
            End Select
        Next

        Dim oSigne As DTOCcb.DhEnum = DTOCcb.DhEnum.Debe
        If oDebit.Eur > oCredit.Eur Then
            oSaldo = oDebit.Substract(oCredit)
            oSigne = DTOCcb.DhEnum.Haber
        Else
            oSaldo = oCredit.Substract(oDebit)
        End If

        Dim retval As DTOCcb = Nothing
        If oSaldo.IsNotZero Then
            retval = DTOCcb.Factory(Me, oSaldo, oCta, oContact, oSigne, oPnd)
            _Items.Add(retval)
        End If
        Return retval
    End Function


    Public Function AddCcb(oAmt As DTOAmt, oCta As DTOPgcCta, oContact As DTOContact, oDh As DTOCcb.DhEnum, Optional oPnd As DTOPnd = Nothing) As DTOCcb
        Dim retval As DTOCcb = DTOCcb.Factory(Me, oAmt, oCta, oContact, oDh, oPnd)
        _Items.Add(retval)
        Return retval
    End Function

    Shared Function Clon(oCca As DTOCca, oUser As DTOUser) As DTOCca
        Dim retval As DTOCca = Factory(oCca.Fch, oUser, oCca.Ccd, oCca.Cdn)
        With retval
            .Concept = oCca.Concept
            .Items = New List(Of DTOCcb)
            For Each oCcb As DTOCcb In oCca.Items
                .AddCcb(oCcb.Amt, oCcb.Cta, oCcb.Contact, oCcb.Dh)
            Next
        End With
        Return retval
    End Function
End Class
