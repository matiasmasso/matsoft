

Public Class Frm_IncidenciaCod
    Private _IncidenciaCod As DTOIncidenciaCod
    Private _Done As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Incidencies
    End Enum

    Public Sub New(value As DTOIncidenciaCod)
        MyBase.New()
        Me.InitializeComponent()
        _IncidenciaCod = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.IncidenciaCod.Load(_IncidenciaCod, exs) Then
            Select Case _IncidenciaCod.Cod
                Case DTOIncidenciaCod.cods.averia
                    Me.Text = "Codi apertura incidencia"
                Case DTOIncidenciaCod.cods.tancament
                    Me.Text = "Codi tancament incidencia"
            End Select
            With _IncidenciaCod
                TextBoxEsp.Text = .Nom.Esp
                TextBoxCat.Text = .Nom.Cat
                TextBoxEng.Text = .Nom.Eng
                TextBoxPor.Text = .Nom.Por
                CheckBoxReposicionParcial.Checked = .ReposicionParcial
                CheckBoxReposicionTotal.Checked = .ReposicionTotal
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
         TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
           TextBoxPor.TextChanged,
            CheckBoxReposicionParcial.CheckedChanged,
             CheckBoxReposicionTotal.CheckedChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _IncidenciaCod
            .Nom = New DTOLangText(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text, TextBoxPor.Text)
            .ReposicionParcial = CheckBoxReposicionParcial.Checked
            .ReposicionTotal = CheckBoxReposicionTotal.Checked
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.IncidenciaCod.Update(_IncidenciaCod, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_IncidenciaCod))
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
            If Await FEB.IncidenciaCod.Delete(_IncidenciaCod, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_IncidenciaCod))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.Incidencies
                If Not _Done Then
                    Dim oQuery = DTOIncidenciaQuery.Factory(Current.Session.User)
                    oQuery.IncludeClosed = True
                    Select Case _IncidenciaCod.Cod
                        Case DTOIncidenciaCod.cods.averia
                            oQuery.Codi = _IncidenciaCod
                        Case DTOIncidenciaCod.cods.tancament
                            oQuery.Tancament = _IncidenciaCod
                    End Select
                    oQuery = Await FEB.Incidencias.Query(exs, oQuery)
                    If exs.Count = 0 Then
                        'Xl_Incidencies1.Load(oQuery.result, oQuery.Customer)
                        _Done = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub
End Class


