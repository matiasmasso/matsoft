Public Class Frm_AeatModel
    Private _AeatModel As DTOAeatModel
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOAeatModel)
        MyBase.New()
        Me.InitializeComponent()
        _AeatModel = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.AeatModel.Load(_AeatModel, exs) Then
            With _AeatModel
                TextBoxNom.Text = .Nom
                TextBoxDsc.Text = .Dsc
                UIHelper.LoadComboFromEnum(ComboBoxCod, GetType(DTOAeatModel.Cods), "(seleccionar codi)", .Cod)
                UIHelper.LoadComboFromEnum(ComboBoxPeriodType, GetType(DTOAeatModel.PeriodTypes), "(seleccionar periodes)", .PeriodType)
                CheckBoxSoloInfo.Checked = .SoloInfo
                CheckBoxVisibleBancs.Checked = .VisibleBancs
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With

            refrescaDocs()

            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub refrescaDocs()
        Dim exs As New List(Of Exception)
        Dim oDocs = Await FEB2.AeatDocs.All(Current.Session.Emp, _AeatModel, exs)
        If exs.Count = 0 Then
            Xl_AeatDocs1.Load(oDocs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxDsc.TextChanged,
          ComboBoxCod.SelectedIndexChanged,
           ComboBoxPeriodType.SelectedIndexChanged,
            CheckBoxVisibleBancs.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _AeatModel
            .Nom = TextBoxNom.Text
            .Dsc = TextBoxDsc.Text
            .Cod = ComboBoxCod.SelectedValue
            .PeriodType = ComboBoxPeriodType.SelectedValue
            .SoloInfo = CheckBoxSoloInfo.Checked
            .VisibleBancs = CheckBoxVisibleBancs.Checked
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.AeatModel.Update(_AeatModel, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_AeatModel))
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
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.AeatModel.Delete(_AeatModel, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_AeatModel))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub Xl_AeatDocs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AeatDocs1.RequestToAddNew
        Dim exs As New List(Of Exception)
        Dim oAeatDoc = Await FEB2.AeatDoc.CreateNext(Current.Session.Emp, _AeatModel, exs)
        If exs.Count = 0 Then
            Dim oFrm_Aeatdoc As New Frm_AeatDoc(oAeatDoc)
            oAeatDoc.Fch = Today
            AddHandler oFrm_Aeatdoc.AfterUpdate, AddressOf refrescaDocs
            oFrm_Aeatdoc.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_AeatDocs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AeatDocs1.RequestToRefresh
        refrescaDocs()
    End Sub
End Class


