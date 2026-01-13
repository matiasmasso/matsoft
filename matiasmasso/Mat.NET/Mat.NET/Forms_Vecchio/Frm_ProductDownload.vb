Public Class Frm_ProductDownload
    Private _ProductDownload As DTOProductDownload
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oProductDownload As DTOProductDownload)
        MyBase.New()
        Me.InitializeComponent()
        _ProductDownload = oProductDownload
        LoadSrcs()
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        With _ProductDownload
            TextBoxTarget.Text = BLL.BLLProduct.Nom(.Product)
            CheckBoxPublicarAlConsumidor.Checked = .PublicarAlConsumidor
            CheckBoxPublicarAlDistribuidor.Checked = .PublicarAlDistribuidor
            CheckBoxObsoleto.Checked = .Obsoleto
            Xl_DocFile1.Load(.DocFile)

            If Not .Src = DTO.DTOProductDownload.Srcs.NotSet Then
                For Each Itm As ListItem In ComboBoxSrcs.Items
                    If Itm.Value = .Src Then
                        ComboBoxSrcs.SelectedItem = Itm
                        Exit For
                    End If
                Next
                ButtonDel.Enabled = True
            End If

            If .DocFile IsNot Nothing Then
                TextBoxNom.Text = .DocFile.Nom
            End If
        End With
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
        CheckBoxObsoleto.CheckedChanged, _
         Xl_DocFile1.AfterFileDropped, _
          ComboBoxSrcs.SelectedIndexChanged, _
           CheckBoxPublicarAlConsumidor.CheckedChanged, _
            CheckBoxPublicarAlDistribuidor.CheckedChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oSelectedItem As MaxiSrvr.ListItem = CType(ComboBoxSrcs.SelectedItem, MaxiSrvr.ListItem)
        If oSelectedItem.Value = DownloadSrc.Ids.NotSet Then
            MsgBox("falta triar una categoría", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            Dim oDocfile As DTODocFile = Xl_DocFile1.Value
            With oDocfile
                .Nom = TextBoxNom.Text
            End With
            With _ProductDownload
                .Src = CType(ComboBoxSrcs.SelectedItem, ListItem).Value
                .PublicarAlConsumidor = CheckBoxPublicarAlConsumidor.Checked
                .PublicarAlDistribuidor = CheckBoxPublicarAlDistribuidor.Checked
                .Obsoleto = CheckBoxObsoleto.Checked
                .DocFile = oDocfile
            End With

            Dim exs As New List(Of Exception)
            If BLL.BLLProductDownload.Update(_ProductDownload, exs) Then
                RaiseEvent AfterUpdate(_ProductDownload, System.EventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If BLL.BLLProductDownload.Delete(_ProductDownload, exs) Then
            RaiseEvent AfterUpdate(_ProductDownload, System.EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub LoadSrcs()
        For Each v As DownloadSrc.Ids In [Enum].GetValues(GetType(DownloadSrc.Ids))
            Dim oItem As New MaxiSrvr.ListItem(v, v.ToString.Replace("_", " "))
            If v = DownloadSrc.Ids.NotSet Then oItem.Text = "(seleccionar categoria)"
            ComboBoxSrcs.Items.Add(oItem)
        Next
    End Sub

End Class