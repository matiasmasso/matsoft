Public Class DTOBancTransferPool
    Inherits DTOBaseGuid
    Property User As DTOUser
    Property BancEmissor As DTOBanc
    Property Fch As Date
    Property Ref As String
    Property Expenses As DTOAmt
    Property Beneficiaris As List(Of DTOBancTransferBeneficiari)
    Property Cca As DTOCca
    Property Pnds As List(Of DTOPnd)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oUser As DTOUser, Optional DtFch As Date = Nothing, Optional oBancEmissor As DTOBanc = Nothing, Optional oExpenses As DTOAmt = Nothing) As DTOBancTransferPool
        Dim retval As New DTOBancTransferPool
        With retval
            .User = oUser
            .Fch = If(DtFch = Nothing, DateTime.Today, DtFch)
            .BancEmissor = oBancEmissor
            .Expenses = oExpenses
            .Beneficiaris = New List(Of DTOBancTransferBeneficiari)
        End With
        Return retval
    End Function

    Public Function Total() As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oBeneficiari In _Beneficiaris
            retval.Add(oBeneficiari.Amt)
        Next
        Return retval
    End Function

    Public Function MsgId() As String
        Dim retval As String = String.Format("{0}.{1:yyyyMMddhhmmss}", _Cca.formattedId(), DateTime.Now)
        Return retval
    End Function

    Public Function DefaultFilename(Optional sBeneficiariNom As String = "") As String
        Dim sufix As String = ""
        If sBeneficiariNom > "" Then
            sufix = sBeneficiariNom
        Else
            If _Beneficiaris.Count = 1 Then
                sufix = _Beneficiaris.First.Contact.Nom
            Else
                Dim firstConcepte As String = _Beneficiaris.First.Concepte
                Dim sameConcepte As Boolean = Not _Beneficiaris.Exists(Function(x) x.Concepte <> firstConcepte)
                If sameConcepte Then
                    sufix = firstConcepte
                End If
            End If
        End If
        Dim retval As String = String.Format("SepaCreditTransfer {0} {1}.xml", _Cca.formattedId(), sufix)
        Return retval
    End Function

    Shared Function AddBeneficiari(oBancTransferPool As DTOBancTransferPool, oCta As DTOPgcCta, oContact As DTOContact, oBankBranch As DTOBankBranch, sAccount As String, oAmt As DTOAmt, sConcepte As String)
        'Quan no hi ha Partida pendent a saldar DTOPnd
        Dim retval As New DTOBancTransferBeneficiari
        With retval
            .Parent = oBancTransferPool
            .Cta = oCta
            .Contact = oContact
            .BankBranch = oBankBranch
            .Account = sAccount
            .Amt = oAmt
            .Concepte = sConcepte
        End With
        oBancTransferPool.Beneficiaris.Add(retval)
        Return retval
    End Function

    Shared Function AddBeneficiari(oBancTransferPool As DTOBancTransferPool, oCta As DTOPgcCta, oContact As DTOContact, oIban As DTOIban, oAmt As DTOAmt, sConcepte As String)
        Dim oBankBranch As DTOBankBranch = Nothing
        Dim sAccount As String = ""
        If oIban IsNot Nothing Then
            oBankBranch = oIban.BankBranch
            sAccount = oIban.Digits
        End If
        Dim retval As New DTOBancTransferBeneficiari
        With retval
            .Parent = oBancTransferPool
            .Cta = oCta
            .Contact = oContact
            .BankBranch = oBankBranch
            .Account = sAccount
            .Amt = oAmt
            .Concepte = sConcepte
        End With
        oBancTransferPool.Beneficiaris.Add(retval)
        Return retval
    End Function

    Shared Sub AddPnd(ByRef oBancTransferPool As DTOBancTransferPool, oPnd As DTOPnd, oBankBranch As DTOBankBranch, sAccount As String, Optional oLang As DTOLang = Nothing)
        If oLang Is Nothing Then oLang = DTOLang.ESP

        Dim oBeneficiari As DTOBancTransferBeneficiari = oBancTransferPool.Beneficiaris.Find(
            Function(x) x.Contact.Equals(oPnd.Contact) And x.Account = sAccount)

        If oBeneficiari Is Nothing Then
            oBeneficiari = New DTOBancTransferBeneficiari
            With oBeneficiari
                .Parent = oBancTransferPool
                .Cta = oPnd.Cta
                .Contact = oPnd.Contact
                .BankBranch = oBankBranch
                .Account = sAccount
                .Amt = DTOAmt.Empty
            End With
            oBancTransferPool.Beneficiaris.Add(oBeneficiari)
        End If

        With oBeneficiari
            .Pnds.Add(oPnd)
            .Concepte = DTOPnd.Concepte(.Pnds)
            Select Case oPnd.Cod
                Case DTOPnd.Codis.Creditor
                    .Amt.Add(oPnd.Amt)
                Case DTOPnd.Codis.Deutor
                    .Amt.Substract(oPnd.Amt)
            End Select
        End With
    End Sub

End Class

Public Class DTOBancTransferBeneficiari
    Inherits DTOBaseGuid

    Property Parent As DTOBancTransferPool
    Property Cta As DTOPgcCta
    Property Contact As DTOContact
    Property Amt As DTOAmt
    Property BankBranch As DTOBankBranch
    Property Account As String
    Property Concepte As String
    Property Pnds As List(Of DTOPnd)

    Property IsOurBankAccount As Boolean

    Public Sub New()
        MyBase.New()
        _Pnds = New List(Of DTOPnd)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Pnds = New List(Of DTOPnd)
    End Sub

    Shared Function Ccbs(value As DTOBancTransferBeneficiari) As List(Of DTOCcb)
        'per Remmitance advice
        Dim oCca As DTOCca = value.Parent.Cca
        Dim retval As List(Of DTOCcb) = oCca.Items.FindAll(Function(x) value.Contact.Equals(x.Contact))
        Return retval
    End Function
End Class
