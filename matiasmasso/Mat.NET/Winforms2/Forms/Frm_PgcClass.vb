Public Class Frm_PgcClass
    Private _PgcClass As DTOPgcClass
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPgcClass)
        MyBase.New()
        Me.InitializeComponent()
        _PgcClass = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.PgcClass.Load(_PgcClass, exs) Then
            UIHelper.LoadComboFromEnum(ComboBoxCod, GetType(DTOPgcClass.Cods))
            With _PgcClass
                If .Parent Is Nothing Then
                    Xl_LookupPgcPlan1.Visible = True
                    LabelPlan.Visible = True
                Else
                    Xl_LookupPgcClassParent.Visible = True
                    LabelParent.Visible = True
                End If

                Xl_LookupPgcPlan1.PgcPlan = .Plan
                Xl_LookupPgcClassParent.PgcClass = .Parent
                ComboBoxCod.SelectedIndex = .Cod
                TextBoxEsp.Text = .Nom.Esp
                TextBoxCat.Text = .Nom.Cat
                TextBoxEng.Text = .Nom.Eng
                NumericUpDownOrd.Value = .Ord
                NumericUpDownLevel.Value = .Level
                CheckBoxHideFigures.Checked = .HideFigures
                'Xl_PgcClassesSumandos.Load(.Sumandos)

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            Xl_LookupPgcPlan1.AfterUpdate,
             Xl_LookupPgcClassParent.AfterUpdate,
        TextBoxEsp.TextChanged,
         TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
           NumericUpDownOrd.ValueChanged,
            NumericUpDownLevel.ValueChanged,
             CheckBoxHideFigures.CheckedChanged,
              Xl_PgcClassesSumandos.AfterUpdate,
               ComboBoxCod.SelectedIndexChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _PgcClass
            .Plan = Xl_LookupPgcPlan1.PgcPlan
            .Parent = Xl_LookupPgcClassParent.PgcClass
            .Nom = New DTOLangText(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text)
            .Ord = NumericUpDownOrd.Value
            .Level = NumericUpDownLevel.Value
            .HideFigures = CheckBoxHideFigures.Checked
            .Cod = ComboBoxCod.SelectedIndex
            '.Sumandos = Xl_PgcClassesSumandos.values
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.PgcClass.Update(_PgcClass, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_PgcClass))
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
            If Await FEB.PgcClass.Delete(_PgcClass, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_PgcClass))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class


