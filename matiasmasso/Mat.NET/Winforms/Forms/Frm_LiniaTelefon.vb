

Public Class Frm_LiniaTelefon
    Private _LiniaTelefon As DTOLiniaTelefon
    Private mAllowEvents As Boolean = False

    Private Enum Tabs
        General
        Downloads
    End Enum

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oLiniaTelefon As DTOLiniaTelefon)
        MyBase.New()
        Me.InitializeComponent()
        _LiniaTelefon = oLiniaTelefon
    End Sub

    Private Async Sub Frm_LiniaTelefon_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.LiniaTelefon.Load(_LiniaTelefon, exs) Then
            Await Refresca()
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        With _LiniaTelefon
            TextBoxNum.Text = .Num
            TextBoxAlias.Text = .Alias
            Xl_Contact1.Contact = .Contact
            TextBoxIcc.Text = .Icc
            TextBoxImei.Text = .Imei
            TextBoxPuk.Text = .Puk
            If .Alta <> Date.MinValue Then
                DateTimePickerAlta.Value = .Alta
            End If

            If .Baixa <> Date.MinValue Then
                CheckBoxBaixa.Checked = True
                DateTimePickerBaixa.Visible = True
                DateTimePickerBaixa.Value = .Baixa
            End If

            CheckBoxPrivat.Checked = .Privat

            Await LoadProductDownloads()

            ButtonDel.Enabled = Not .IsNew
        End With
    End Function

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNum.TextChanged,
         TextBoxAlias.TextChanged,
          Xl_Contact1.AfterUpdate,
           TextBoxIcc.TextChanged,
            TextBoxImei.TextChanged,
             TextBoxPuk.TextChanged,
           DateTimePickerAlta.ValueChanged,
            DateTimePickerBaixa.ValueChanged,
             CheckBoxPrivat.CheckedChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxBaixa_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxBaixa.CheckedChanged
        If mAllowEvents Then
            DateTimePickerBaixa.Visible = CheckBoxBaixa.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _LiniaTelefon
            .Num = TextBoxNum.Text
            .Alias = TextBoxAlias.Text
            .Contact = Xl_Contact1.Contact
            .Icc = TextBoxIcc.Text
            .Imei = TextBoxImei.Text
            .Puk = TextBoxPuk.Text
            .Alta = DateTimePickerAlta.Value.Date
            If CheckBoxBaixa.Checked Then
                .Baixa = DateTimePickerBaixa.Value.Date
            Else
                .Baixa = Date.MinValue
            End If
            .Privat = CheckBoxPrivat.Checked
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.LiniaTelefon.Update(_LiniaTelefon, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_LiniaTelefon))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("Eliminem la linia?", MsgBoxStyle.OkCancel)
        If rc = vbOK Then
            Dim exs As New List(Of Exception)
            If Await FEB2.LiniaTelefon.Delete(_LiniaTelefon, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_LiniaTelefon))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
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

    Private Async Sub LoadProductDownloads(ByVal sender As Object, ByVal e As System.EventArgs)
        Await LoadProductDownloads()
    End Sub

    Private Async Function LoadProductDownloads() As Task
        Dim exs As New List(Of Exception)
        Dim oDownloads = Await FEB2.ProductDownloads.All(_LiniaTelefon, exs)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_ProductDownloads1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToAddNew
        Dim oProductDownload As New DTOProductDownload
        Dim exs As New List(Of Exception)

        If oProductDownload.AddTarget(_LiniaTelefon, exs) Then
            Dim oFrm As New Frm_ProductDownload(oProductDownload)
            AddHandler oFrm.AfterUpdate, AddressOf LoadProductDownloads
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductDownloads1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductDownloads1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oDownloads = Await FEB2.ProductDownloads.All(_LiniaTelefon, exs)
        If exs.Count = 0 Then
            Xl_ProductDownloads1.Load(oDownloads)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
End Class