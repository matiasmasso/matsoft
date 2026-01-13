Public Class Frm_Immoble

    Private _Immoble As DTOImmoble
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Downloads
        Inventari
    End Enum

    Public Sub New(value As DTOImmoble)
        MyBase.New()
        Me.InitializeComponent()
        _Immoble = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Immoble.Load(_Immoble, exs) Then
            With _Immoble
                TextBoxNom.Text = .Nom
                If .Address IsNot Nothing Then
                    TextBoxAdr.Text = .Address.Text
                    Xl_LookupZip1.Load(.Address.Zip)
                End If
                TextBoxCadastre.Text = .Cadastre
                If .FchFrom > DateTimePickerFchFrom.MinDate Then
                    DateTimePickerFchFrom.Value = .FchFrom
                End If
                If .FchTo > DateTimePickerFchTo.Value Then
                    CheckBoxVenda.Checked = True
                    DateTimePickerFchTo.Value = .FchTo
                    DateTimePickerFchTo.Visible = True
                End If
                ComboBoxTitularitat.SelectedIndex = .Titularitat
                Xl_Percent1.Load(.Part, 2)
                Xl_TextBoxNumSuperficie.Value = .Superficie
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxAdr.TextChanged,
          Xl_LookupZip1.AfterUpdate,
           TextBoxCadastre.TextChanged,
            DateTimePickerFchFrom.ValueChanged,
             DateTimePickerFchTo.ValueChanged,
              Xl_TextBoxNumSuperficie.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxVenda_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxVenda.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxVenda.Checked
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Immoble
            .Emp = New Models.Base.IdNom(GlobalVariables.Emp.Id)
            .Nom = TextBoxNom.Text
            .Address = New DTOAddress
            With .Address
                .Text = TextBoxAdr.Text
                .Zip = Xl_LookupZip1.Zip
            End With
            .Titularitat = ComboBoxTitularitat.SelectedIndex
            .Part = Xl_Percent1.Value
            .Superficie = Xl_TextBoxNumSuperficie.Value
            .Cadastre = TextBoxCadastre.Text
            .FchFrom = DateTimePickerFchFrom.Value
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.Immoble.Update(_Immoble, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Immoble))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest immoble?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.Immoble.Delete(_Immoble, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Immoble))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Downloads
                Static LoadedDocFileSrcs As Boolean
                If Not LoadedDocFileSrcs Then
                    refrescaDocFileSrcs()
                    LoadedDocFileSrcs = True
                End If
            Case Tabs.Inventari
                Static LoadedInventari As Boolean
                If Not LoadedInventari Then
                    refrescaInventari()
                    LoadedInventari = True
                End If
        End Select
    End Sub

    Private Async Sub refrescaDocFileSrcs()
        Dim exs As New List(Of Exception)
        Dim oDocFileSrcs = Await FEB.DocFileSrcs.All(_Immoble, exs)
        If exs.Count = 0 Then
            Xl_DocfileSrcs1.Load(oDocFileSrcs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub refrescaInventari()
        Dim exs As New List(Of Exception)
        Dim oInventariItems = Await FEB.InventariItems.All(exs, _Immoble)
        If exs.Count = 0 Then
            Xl_InventariItems1.Load(oInventariItems)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToRefresh
        refrescaDocFileSrcs()
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToAddNew
        Dim oDocfileSrc As New DTODocFileSrc()
        With oDocfileSrc
            .Cod = DTODocFile.Cods.Download
            .Src = _Immoble
            .Docfile = e.Argument
        End With
        Dim oFrm As New Frm_DocfileSrc(oDocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaDocFileSrcs
        oFrm.Show()
    End Sub

    Private Sub Xl_InventariItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_InventariItems1.RequestToRefresh
        refrescaInventari()
    End Sub

    Private Sub Xl_InventariItems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_InventariItems1.RequestToAddNew
        Dim oInventariItem = DTOImmoble.InventariItem.factory(_Immoble)
        Dim oFrm As New Frm_InventariItem(oInventariItem)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaInventari
        oFrm.Show()
    End Sub

End Class


