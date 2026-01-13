Public Class Frm_Zip
    Private _Zip As DTOZip
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Tabs
        General
        Contacts
        Albarans
    End Enum

    Public Sub New(ByVal oZip As DTOZip)
        MyBase.New()
        Me.InitializeComponent()
        _Zip = oZip
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With _Zip
            TextBoxLocation.Text = BLL.BLLLocation.FullNom(.Location, BLL.BLLApp.Lang)
            TextBoxZipCod.Text = .ZipCod
            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxZipCod.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Zip
            .ZipCod = TextBoxZipCod.Text.Trim

            Dim exs As New List(Of Exception)
            If BLL.BLLZip.Update(_Zip, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Zip))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al grabar codi postal")
            End If
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If BLL.BLLZip.Delete(_Zip, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Zip))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al eliminar el codi postal")
        End If
    End Sub


    Private Sub RefrescaLocation()
        TextBoxLocation.Text = BLL.BLLLocation.FullNom(_Zip.Location, BLL.BLLApp.Lang)
        RaiseEvent AfterUpdate(_Zip, EventArgs.Empty)
    End Sub

    Private Sub TextBoxLocation_DoubleClick(sender As Object, e As System.EventArgs) Handles TextBoxLocation.DoubleClick
        Dim oFrm As New Frm_Location(_Zip.Location)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaLocation
        oFrm.Show()
    End Sub


    Private Sub TextBoxLocation_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TextBoxLocation.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            SetContextMenu()
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _Zip.Location IsNot Nothing Then
            Dim oMenu_Location As New Menu_Location(_Zip.Location)
            AddHandler oMenu_Location.AfterUpdate, AddressOf RefrescaLocation
            oContextMenu.Items.AddRange(oMenu_Location.Range)
        End If

        TextBoxLocation.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Contacts
                Static BlDoneContacts As Boolean
                If Not BlDoneContacts Then
                    BlDoneContacts = True
                    'Xl_Zip_Contacts1.Zip = _Zip
                End If
            Case Tabs.Albarans
                Static BlDoneAlbarans As Boolean
                If Not BlDoneAlbarans Then
                    BlDoneAlbarans = True
                    Dim oArea As New area(_Zip)
                    'Xl_AreaAlbs1.LoadControl(oArea, True)
                End If
        End Select
    End Sub
End Class
