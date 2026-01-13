Public Class Xl_IbanDigits
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Shadows Event DoubleClick(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(sIbanDigits As String)
        TextBox1.Text = BLL.BLLIban.Formated(sIbanDigits)
        RefreshRequest()
        _AllowEvents = True
    End Sub

    Public Shadows Sub Load(oIban As DTOIban)
        If oIban IsNot Nothing Then
            TextBox1.Text = BLL.BLLIban.Formated(oIban.Digits)
        End If
        RefreshRequest()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As String
        Get
            Dim retval As String = BLL.BLLIbanStructure.CleanCcc(TextBox1.Text)
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

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        If _AllowEvents Then
            RefreshRequest()
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
        End If
    End Sub

    Private Sub Do_Zoom()
        If TextBox1.Text.Length >= 12 Then
            Do_Branch()
        ElseIf TextBox1.Text >= 8 Then
            Do_Bank()
        End If
    End Sub

    Private Sub Do_Bank()
        Dim oBank As DTOBank = BLL.BLLIbanStructure.GetBank(TextBox1.Text)
        Dim oFrm As New Frm_Bank(oBank)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Branch()
        Dim oBranch As DTOBankBranch = BLL.BLLIbanStructure.GetBankBranch(TextBox1.Text)
        Dim oFrm As New Frm_BankBranch(oBranch)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest()
        PictureBox1.Image = BLL.BLLIban.Img(TextBox1.Text)
        SetContextMenu()
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        RaiseEvent DoubleClick(sender, e)
    End Sub


End Class
