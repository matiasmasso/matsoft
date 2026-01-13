Public Class Xl_Iban
    Private _Iban As DTOIban
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToChange(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oIban As DTOIban)
        _Iban = oIban
        If _Iban IsNot Nothing Then
            Refresca()
            SetContextMenu()
        End If
    End Sub

    Public ReadOnly Property Value As DTOIban
        Get
            Return _Iban
        End Get
    End Property

    Private Sub Refresca()
        If _Iban IsNot Nothing Then
            PictureBox1.Image = BLL.BLLIban.Img(_Iban.Digits)
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenu_Digits As New Menu_IbanDigits(_Iban.Digits)
        AddHandler oMenu_Digits.AfterUpdate, AddressOf Refresca
        oContextMenu.Items.AddRange(oMenu_Digits.Range)
        oContextMenu.Items.Add("-")
        oContextMenu.Items.Add("canviar", Nothing, AddressOf Do_Change)

        PictureBox1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Change()
        RaiseEvent RequestToChange(Me, MatEventArgs.Empty)
        'Dim oContact As DTOContact = _Iban.Titular
        'Dim oFrm As New Frm_Contact_Ibans(oContact)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        RaiseEvent RequestToChange(Me, MatEventArgs.Empty)
    End Sub
End Class
