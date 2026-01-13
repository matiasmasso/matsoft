

Public Class Frm_ContractCodi
    Private mCodi As ContractCodi = Nothing
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCodi As contractCodi)
        MyBase.New()
        Me.InitializeComponent()
        mCodi = oCodi
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        If mCodi.Id = 0 Then
            Me.Text = "NOU CODI DE CONTRACTE"
        Else
            Me.Text = "CODI #" & mCodi.Id & " DE CONTRACTE"
        End If
        TextBoxNom.Text = mCodi.Nom
        CheckBoxAmortitzable.Checked = mCodi.Amortitzable

        ButtonDel.Enabled = mCodi.Exists And mCodi.AllowDelete
        mallowevents = True
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged, _
     CheckBoxAmortitzable.CheckedChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mCodi
            .Nom = TextBoxNom.Text
            .Amortitzable = CheckBoxAmortitzable.Checked
            .Update()
        End With
        RaiseEvent AfterUpdate(mCodi, EventArgs.Empty)
        Me.Close()
    End Sub


    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest codi?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If mCodi.AllowDelete Then
                mCodi.Delete()
                RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
                Me.Close()
            Else
                MsgBox("Cal buidar primer els contractes d'aquest codi", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub
End Class