Public Class Frm_Zip
    Private _Zip As DTOZip
    Private _DoneContacts As Boolean
    Private _DoneAlbarans As Boolean
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
            Me.Text = .FullNom(Current.Session.Lang)
            TextBoxLocation.Text = .location.FullNom(Current.Session.Lang)
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

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Zip
            .ZipCod = TextBoxZipCod.Text.Trim

            Dim exs As New List(Of Exception)
            UIHelper.ToggleProggressBar(Panel1, True)
            If Await FEB.Zip.Update(_Zip, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Zip))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        End With
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If Await FEB.Zip.Delete(_Zip, exs) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub RefrescaLocation()
        TextBoxLocation.Text = _Zip.Location.FullNom(DTOApp.current.lang)
        RaiseEvent AfterUpdate(_Zip, MatEventArgs.Empty)
    End Sub

    Private Sub TextBoxLocation_DoubleClick(sender As Object, e As System.EventArgs) Handles TextBoxLocation.DoubleClick
        Dim oFrm As New Frm_Location(_Zip.Location)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaLocation
        oFrm.Show()
    End Sub


    Private Sub TextBoxLocation_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TextBoxLocation.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
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

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Contacts
                If Not _DoneContacts Then
                    _DoneContacts = True
                    Await refrescaContacts()
                End If
            Case Tabs.Albarans
                If Not _DoneAlbarans Then
                    _DoneAlbarans = True
                    Await refrescaDeliveries()
                End If
        End Select
    End Sub

    Private Async Sub Xl_AreaDeliveries1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaDeliveries1.RequestToRefresh
        Await refrescaDeliveries()
    End Sub

    Private Async Function refrescaDeliveries() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oDeliveries = Await FEB.Deliveries.All(exs, GlobalVariables.Emp, _Zip)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_AreaDeliveries1.Load(oDeliveries)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_AreaContacts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaContacts1.RequestToRefresh
        Await refrescaContacts()
    End Sub

    Private Async Function refrescaContacts() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oContacts = Await FEB.Contacts.All(exs, Current.Session.Emp, _Zip)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_AreaContacts1.Load(oContacts)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub ButtonDel_CursorChanged(sender As Object, e As EventArgs) Handles ButtonDel.CursorChanged

    End Sub
End Class
