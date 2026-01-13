Public Class Frm_Estate
    Private _Estate As DTOEstate
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Downloads
    End Enum

    Public Sub New(value As DTOEstate)
        MyBase.New()
        Me.InitializeComponent()
        _Estate = value
    End Sub

    Private Async Sub Frm_Estate_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Estate.Load(exs, _Estate) Then
            With _Estate
                TextBoxNom.Text = .Nom
                TextBoxCadastral.Text = .Cadastral
                Await Xl_Contact21.Load(.Owner)
                If .FchFrom > DateTimePickerFchFrom.MinDate Then
                    DateTimePickerFchFrom.Value = .FchFrom
                End If
                If .FchTo > DateTimePickerFchTo.MinDate Then
                    CheckBoxBaixa.Checked = True
                    DateTimePickerFchTo.Visible = True
                    DateTimePickerFchTo.Value = .FchTo
                End If
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
        TextBoxCadastral.TextChanged,
        Xl_Contact21.AfterUpdate,
        DateTimePickerFchFrom.ValueChanged,
        DateTimePickerFchTo.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub CheckBoxBaixa_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxBaixa.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxBaixa.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Estate
            .Nom = TextBoxNom.Text
            .Cadastral = TextBoxCadastral.Text
            .Owner = Xl_Contact21.Contact
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxBaixa.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.Estate.Update(exs, _Estate) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Estate))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.Estate.Delete(exs, _Estate) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Estate))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case DirectCast(TabControl1.SelectedIndex, Tabs)
            Case Tabs.General
            Case Tabs.Downloads
                Static DoneDownloads As Boolean
                If Not DoneDownloads Then
                    DoneDownloads = True
                    LoadProductDownloads(sender, e)
                End If
        End Select
    End Sub

    Private Async Sub LoadProductDownloads(sender As Object, e As EventArgs)
        Await LoadProductDownloads()
    End Sub

    Private Async Function LoadProductDownloads() As Task
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        Dim oDownloads = Await FEB2.ProductDownloads.All(_Estate, exs)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
            UIHelper.ToggleProggressBar(PanelButtons, False)
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim oProductDownload As New DTOProductDownload
        Dim exs As New List(Of Exception)

        If oProductDownload.AddTarget(_Estate, exs) Then
            Dim oFrm As New Frm_ProductDownload(oProductDownload)
            AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        Await LoadProductDownloads()
    End Sub

End Class


