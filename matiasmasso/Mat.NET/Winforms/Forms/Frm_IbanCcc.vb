Public Class Frm_IbanCcc
    Private _PreviousIban As DTOIban
    Private _Iban As DTOIban
    Private _IbanStructures As List(Of DTOIbanStructure)
    Private _BankBranch As DTOBankBranch
    Private _Bank As DTOBank

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(oIban As DTOIban, Optional oPreviousIban As DTOIban = Nothing)
        InitializeComponent()
        _Iban = oIban
        _PreviousIban = oPreviousIban
    End Sub

    Private Async Sub Frm_IbanCcc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        _IbanStructures = Await FEB2.IbanStructures.All(exs)
        If exs.Count = 0 Then
            TextBoxCcc.Text = _Iban.Digits
            Dim src As String = CleanSrc()
            If src.Length >= 2 Then Dim oStructure As DTOIbanStructure = GetStructure()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TextBoxCcc_TextChanged(sender As Object, e As EventArgs) Handles TextBoxCcc.TextChanged
        Dim exs As New List(Of Exception)
        With _Iban
            .IbanStructure = GetStructure()
            .BankBranch = Await GetBankBranch(exs)
            .Digits = CleanSrc()
            .IsLoaded = True
        End With
        PictureBox1.Image = LegacyHelper.ImageHelper.Converter(Await FEB2.Iban.Img(exs, _Iban, Current.Session.Lang))
    End Sub


    Private Function CleanSrc() As String
        Dim retval As String = DTOIban.CleanCcc(TextBoxCcc.Text)
        Return retval
    End Function

    Private Async Function GetBankBranch(exs As List(Of Exception)) As Task(Of DTOBankBranch)
        Dim oBank As DTOBank = Await GetBank(exs)
        If oBank Is Nothing Then
            _BankBranch = Nothing
        Else
            Dim oStructure As DTOIbanStructure = GetStructure()
            If oStructure IsNot Nothing Then
                Dim src As String = CleanSrc()
                Dim iBankLen As Integer = oStructure.BankPosition + oStructure.BankLength
                Dim iBranchLen As Integer = oStructure.BranchPosition + oStructure.BranchLength

                If (src.Length = iBranchLen Or (_BankBranch Is Nothing And src.Length >= iBranchLen)) Then
                    Dim Id As String = src.Substring(oStructure.BranchPosition, oStructure.BranchLength)
                    _BankBranch = Await FEB2.BankBranch.Find(oBank, Id, exs)
                    If _BankBranch Is Nothing Then
                        _BankBranch = New DTOBankBranch
                        With _BankBranch
                            .Bank = oBank
                            .Id = Id
                        End With
                    End If
                ElseIf src.Length = iBankLen Or src.Length = iBranchLen - 1 Or (_BankBranch Is Nothing And src.Length >= iBankLen And src.Length < iBranchLen) Then
                    _BankBranch = New DTOBankBranch
                    _BankBranch.Bank = oBank
                ElseIf src.Length = iBankLen - 1 Or (_BankBranch Is Nothing And src.Length < iBankLen) Then
                    _BankBranch = Nothing
                End If

                Select Case src.Length
                    Case iBankLen - 1
                        _BankBranch = Nothing
                    Case iBankLen, iBranchLen - 1
                        _BankBranch = New DTOBankBranch
                        _BankBranch.Bank = oBank
                    Case iBranchLen
                End Select
            End If
        End If
        Return _BankBranch
    End Function

    Private Async Function GetBank(exs As List(Of Exception)) As Task(Of DTOBank)
        Dim oStructure As DTOIbanStructure = GetStructure()
        If oStructure IsNot Nothing Then
            Dim src As String = CleanSrc()

            Dim iLen As Integer = oStructure.BankPosition + oStructure.BankLength
            If src.Length = iLen Or (_Bank Is Nothing And src.Length >= iLen) Then
                Dim oCountry As DTOCountry = oStructure.Country
                Dim Id As String = src.Substring(oStructure.BankPosition, oStructure.BankLength)
                _Bank = Await FEB2.Bank.FromCodi(oCountry, Id, exs)
                PictureBox1.Visible = True
            ElseIf src.Length = iLen - 1 Then
                _Bank = Nothing
                PictureBox1.Visible = False
            End If

        End If
        Return _Bank
    End Function

    Private Function GetStructure() As DTOIbanStructure
        Static retval As DTOIbanStructure = Nothing
        Dim src As String = CleanSrc()

        If ((retval Is Nothing And src.Length >= 2) Or src.Length = 2) Then
            Dim Iso As String = src.Substring(0, 2)
            retval = _IbanStructures.Find(Function(x) x.Country.ISO = Iso)
        ElseIf src.Length = 1 Then
            retval = Nothing
        End If

        Return retval
    End Function


    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If _Iban.fchFrom = Nothing Then _Iban.fchFrom = Today
        _Iban.Emp = GlobalVariables.Emp
        If Await FEB2.Iban.Update(_Iban, exs) Then
            If _PreviousIban IsNot Nothing Then
                _PreviousIban.FchTo = _Iban.FchFrom.AddDays(-1)
                If Not Await FEB2.Iban.Update(_PreviousIban, exs) Then
                    UIHelper.ToggleProggressBar(Panel1, False)
                    UIHelper.WarnError("error al fer caducar l'antic compte")
                End If
            End If
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Iban))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub


End Class