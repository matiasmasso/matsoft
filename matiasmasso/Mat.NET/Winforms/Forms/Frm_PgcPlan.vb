

Public Class Frm_PgcPlan
    Private _PgcPlan As DTOPgcPlan
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPgcPlan)
        MyBase.New()
        Me.InitializeComponent()
        _PgcPlan = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.PgcPlan.Load(_PgcPlan, exs) Then
            With _PgcPlan
                If .IsNew Then
                    Me.Text = "NOU PLA DE COMPTABILITAT"
                    ButtonDel.Enabled = False
                Else
                    Me.Text = "PLA DE COMPTABILITAT"
                    ButtonDel.Enabled = True
                End If
                TextBoxNom.Text = .Nom
                'ComboBoxLastPgcPlan.SelectedValue = .LastPlan.Id
                TextBoxYearFrom.Text = IIf(.YearFrom > 0, .YearFrom, "")
                TextBoxYearTo.Text = IIf(.YearTo > 0, .YearTo, "")

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged,
     ComboBoxLastPgcPlan.SelectedIndexChanged,
      TextBoxYearFrom.TextChanged,
       TextBoxYearTo.TextChanged
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _PgcPlan
            .Nom = TextBoxNom.Text
            '.LastPlan = New PgcPlan(ComboBoxLastPgcPlan.SelectedValue)
            If IsNumeric(TextBoxYearFrom.Text) Then
                .YearFrom = TextBoxYearFrom.Text
            End If
            If IsNumeric(TextBoxYearTo.Text) Then
                .YearTo = TextBoxYearTo.Text
            End If
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.PgcPlan.Update(_PgcPlan, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_PgcPlan))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim sErr As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest pla?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.PgcPlan.Delete(_PgcPlan, exs) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox(sErr, MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub
End Class