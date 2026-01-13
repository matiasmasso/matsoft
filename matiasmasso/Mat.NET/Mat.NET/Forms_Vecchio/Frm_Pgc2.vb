Public Class Frm_Pgc2
    Private _AllowEvents As Boolean

    Private Sub Frm_Pgc2_Load(sender As Object, e As EventArgs) Handles Me.Load

        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca(Optional oSelectedEpg As DTOPgcEpgBase = Nothing)
        Dim oEpgs As List(Of DTOPgcEpgBase) = BLL_PgcEpgBases.FullTree
        Dim oMissingCtas As List(Of DTOPgcCta) = BLL_PgcCtas.MissingEpgCtas
        If oMissingCtas.Count > 0 Then
            Dim oCaption As New DTOPgcEpgBase
            With oCaption
                .NomEsp = "(sin epigrafe)"
                .Children = New List(Of DTOPgcEpgBase)
                .Children.AddRange(oMissingCtas)
            End With
            oEpgs.Add(oCaption)
        End If

        Xl_PgcEpgs1.Load(oEpgs, BLL.BLLSession.Current.Lang, oSelectedEpg)
    End Sub

    Private Sub Xl_PgcEpgs1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PgcEpgs1.RequestToRefresh
        refresca(e.Argument)
    End Sub


End Class