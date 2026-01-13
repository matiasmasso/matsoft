Public Class Frm_Download
    Private _Download As Download
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oDownload As Download)
        MyBase.New()
        Me.InitializeComponent()
        _Download = oDownload
        LoadSrcs()
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        With _Download
            ComboBoxSrcs.SelectedValue = .Src
            Xl_DocFile1.Load(.DocFile)
            CheckBoxObsolet.Checked = .Obsoleto
            ButtonDel.Enabled = Not .IsNew
        End With
    End Sub

    Private Sub LoadSrcs()
        UIHelper.LoadComboFromEnum(ComboBoxSrcs, GetType(Download.Srcs))
    End Sub

    Private Sub ControlChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         ComboBoxSrcs.SelectedValueChanged, _
         CheckBoxObsolet.CheckedChanged, _
          Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If ComboBoxSrcs.SelectedValue = 0 Then
            MsgBox("Cal especificar un codi del desplegable", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            With _Download
                If Xl_DocFile1.IsDirty Then
                    .DocFile = Xl_DocFile1.Value
                End If
                .Src = ComboBoxSrcs.SelectedValue
                .Obsoleto = CheckBoxObsolet.Checked
            End With

            Dim exs as New List(Of exception)
            If DownloadLoader.Update(_Download, exs) Then
                RaiseEvent AfterUpdate(_Download, New System.EventArgs)
                Me.Close()
            Else
                UIHelper.WarnError( exs, "error al desar fitxer")
            End If
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        If DownloadLoader.Delete(_Download, exs) Then
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al eliminar fitxer")
        End If
    End Sub
End Class