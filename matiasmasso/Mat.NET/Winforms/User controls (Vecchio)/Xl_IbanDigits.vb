Public Class Xl_IbanDigits
    Private _AllowEvents As Boolean
    Property BankBranch As DTOBankBranch

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Shadows Event DoubleClick(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Async Function Load(sIbanDigits As String, Optional oBankBranch As DTOBankBranch = Nothing) As Task
        TextBox1.Text = DTOIban.Formated(sIbanDigits)
        _BankBranch = oBankBranch
        Await RefreshRequest()
        _AllowEvents = True
    End Function

    Public Sub Clear()
        TextBox1.Text = ""
    End Sub

    Public Shadows Async Function Load(oIban As DTOIban) As Task
        If oIban IsNot Nothing Then
            Dim exs As New List(Of Exception)
            TextBox1.Text = DTOIban.Formated(oIban.Digits)
            If oIban.BankBranch Is Nothing Then
                oIban.BankBranch = Await FEB2.Iban.GetBankBranchFromDigits(oIban, exs)
                If exs.Count > 0 Then UIHelper.WarnError(exs)
            End If
            _BankBranch = oIban.BankBranch
        End If
        Await RefreshRequest()
        _AllowEvents = True
    End Function

    Public ReadOnly Property Value As String
        Get
            Dim retval As String = DTOIban.CleanCcc(TextBox1.Text)
            Return retval
        End Get
    End Property

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu_Digits As New Menu_IbanDigits(TextBox1.Text)
        AddHandler oMenu_Digits.AfterUpdate, AddressOf RefreshRequest
        oContextMenu.Items.AddRange(oMenu_Digits.Range)
        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If _AllowEvents Then
            Dim exs As New List(Of Exception)
            _BankBranch = Await FEB2.IbanStructure.GetBankBranch(TextBox1.Text, exs)
            If exs.Count = 0 Then
                Await RefreshRequest()
                RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Do_Zoom()
        If TextBox1.Text.Length >= 12 Then
            Do_Branch()
        ElseIf TextBox1.Text >= 8 Then
            Await Do_Bank()
        End If
    End Sub

    Private Async Function Do_Bank() As Task
        Dim exs As New List(Of Exception)
        Dim oBank = Await FEB2.IbanStructure.GetBank(TextBox1.Text, exs)
        If exs.Count = 0 Then
            Dim oFrm As New Frm_Bank(oBank)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Do_Branch()
        Dim oFrm As New Frm_BankBranch(_BankBranch)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Await RefreshRequest()
    End Sub

    Private Async Function RefreshRequest() As Task
        Dim exs As New List(Of Exception)
        If TextBox1.Text = "" Then
            PictureBox1.Image = Nothing
        Else
            PictureBox1.Image = LegacyHelper.ImageHelper.Converter(Await FEB2.Iban.Img(exs, TextBox1.Text, Current.Session.Lang))
            If exs.Count = 0 Then
                SetContextMenu()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Function

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        RaiseEvent DoubleClick(sender, e)
    End Sub


End Class
