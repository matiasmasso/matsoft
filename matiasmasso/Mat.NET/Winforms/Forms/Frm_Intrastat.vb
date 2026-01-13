Public Class Frm_Intrastat
    Private _Intrastat As DTOIntrastat
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOIntrastat)
        MyBase.New()
        Me.InitializeComponent()
        _Intrastat = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Intrastat.Load(_Intrastat, exs) Then
            With _Intrastat
                Xl_YearMonth1.YearMonth = .YearMonth
                ComboBoxFlujo.SelectedIndex = .Flujo
                NumericUpDownOrd.Value = .Ord
                TextBoxCsv.Text = .Csv
                TextBoxPartides.Text = Format(.Partidas.Count, "#,###")
                TextBoxUnits.Text = Format(.Partidas.Sum(Function(x) x.UnidadesSuplementarias), "#,###")
                TextBoxKgs.Text = Format(.Partidas.Sum(Function(x) x.Kg), "#,##0.00 Kg")
                TextBoxEurs.Text = Format(.Partidas.Sum(Function(x) x.ImporteFacturado), "#,##0.00 €")
                Xl_DocFile1.Load(.DocFile)
                Xl_IntrastatPartidas1.Load(_Intrastat)
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            Xl_YearMonth1.AfterUpdate,
             ComboBoxFlujo.SelectedIndexChanged,
              NumericUpDownOrd.ValueChanged,
               TextBoxCsv.TextChanged,
                Xl_DocFile1.AfterFileDropped

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Intrastat
            .Ord = NumericUpDownOrd.Value
            .Csv = TextBoxCsv.Text

            If Xl_DocFile1.IsDirty Then
                .DocFile = Xl_DocFile1.Value
            End If
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Intrastat.Update(_Intrastat, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Intrastat))
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
            If Await FEB2.Intrastat.Delete(_Intrastat, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Intrastat))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Sub DesarFitxerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesarFitxerToolStripMenuItem.Click
        Menu_Intrastat.SaveFile(_Intrastat)
    End Sub
End Class