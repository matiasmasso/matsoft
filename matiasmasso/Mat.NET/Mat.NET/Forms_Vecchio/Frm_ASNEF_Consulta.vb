

Public Class Frm_ASNEF_Consulta
    Private mConsulta As AsnefConsulta
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oConsulta As AsnefConsulta)
        MyBase.New()
        Me.InitializeComponent()
        mConsulta = oConsulta
        refresca()
    End Sub

    Private Sub Refresca()
        With mConsulta
            TextBoxCliNom.Text = .Contact.Nom
            Xl_Contact_Logo1.Contact = .Contact
            TextBoxObs.Text = .Obs
            TextBoxCreated.Text = .CreatedText
            Select Case .Cod
                Case AsnefConsulta.Cods.Clean
                    RadioButtonClean.Checked = True
                Case AsnefConsulta.Cods.Dirty
                    RadioButtonDirty.Checked = True
            End Select
            ButtonDel.Enabled = .Exists
        End With
        mAllowEvents = True
    End Sub

  

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If CheckErrors() Then
            With mConsulta
                .Obs = TextBoxObs.Text
                .Cod = IIf(RadioButtonClean.Checked, AsnefConsulta.Cods.Clean, AsnefConsulta.Cods.Dirty)
                .Update(root.Usuari)
            End With
            RaiseEvent AfterUpdate(mConsulta, EventArgs.Empty)
            Me.Close()
        End If
    End Sub

    Private Function CheckErrors() As Boolean
        Dim RetVal As Boolean = False
        If mConsulta.Exists Then
            MsgBox("no está permés modificar consultes ja registrades. Si ens hem equivocat cal eliminar-les i tornar-les a crear", MsgBoxStyle.Exclamation, "MAT.NET")
        Else
            If RadioButtonDirty.Checked Then
                If TextBoxObs.Text = "" Then
                    MsgBox("Cal registrar observacions en cas de haver incidencies", MsgBoxStyle.Exclamation, "MAT.NET")
                Else
                    RetVal = True
                End If
            ElseIf RadioButtonClean.Checked Then
                RetVal = True
            Else
                MsgBox("Cal marcar una de les dues opcions de incidencies", MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
        Return RetVal
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem la consulta?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            mConsulta.Delete()
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
            Me.Close()
        End If
    End Sub
End Class