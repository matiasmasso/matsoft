

Public Class Frm_IncidenciaCod
    Private _Cod As DTOIncidenciaCod
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        Id
        Fch
        ArtNom
    End Enum

    Public WriteOnly Property Cod() As DTOIncidenciaCod
        Set(ByVal value As DTOIncidenciaCod)
            _Cod = value
            If _Cod.Id = 0 Then
                Select Case _Cod.Cod
                    Case DTOIncidenciaCod.cods.averia
                        Me.Text = "NOU CODI D'INCIDENCIA"
                    Case DTOIncidenciaCod.cods.tancament
                        Me.Text = "NOU CODI DE TANCAMENT DE INCIDENCIA"
                End Select
            Else
                Select Case _Cod.Cod
                    Case DTOIncidenciaCod.cods.averia
                        Me.Text = "CODI " & _Cod.Id & " DE INCIDENCIA"
                    Case DTOIncidenciaCod.cods.tancament
                        Me.Text = "CODI " & _Cod.Id & " DE TANCAMENT DE INCIDENCIA"
                End Select
                If Not _Cod.IsNew Then ButtonDel.Enabled = True
            End If
            TextBoxEsp.Text = _Cod.Esp
            TextBoxEng.Text = _Cod.Eng
            LoadGrid()
            _AllowEvents = True
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT I.Id, I.Fch, " _
        & "(CASE WHEN BrandNom IS NULL THEN '' ELSE (CASE WHEN CategoryNom IS NULL THEN BrandNom ELSE (CASE WHEN SKUNom IS NULL THEN BrandNom+' '+CategoryNom ELSE BrandNom+' '+CategoryNom+' '+SkuNom END) END) END) AS NOM " _
        & "FROM INCIDENCIES AS I " _
        & "LEFT OUTER JOIN Product2 ON I.ProductGuid = Product2.Guid " _
        & "WHERE I.Cod=@COD " _
        & "ORDER BY I.Id DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@COD", _Cod.Id)
        With DataGridView1
            .DataSource = oDs.Tables(0)
            With .Columns(Cols.Id)
                .Width = 50
            End With
            With .Columns(Cols.Fch)
                .Width = 80
            End With
            With .Columns(Cols.ArtNom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxEsp.TextChanged, _
    TextBoxEng.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Cod
            .Esp = TextBoxEsp.Text
            .Eng = TextBoxEng.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLIncidenciaCod.Update(_Cod, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cod))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest codi?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLIncidenciaCod.Delete(_Cod, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub
End Class