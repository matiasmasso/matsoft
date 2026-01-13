

Public Class Frm_PncRepCom
    Private mLineItmPnc As LineItmPnc
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property LineItmPnc() As LineItmPnc
        Set(ByVal value As LineItmPnc)
            mLineItmPnc = value
            LoadReps()
            With mLineItmPnc
                TextBoxCliNom.Text = .Pdc.Client.Nom
                PictureBoxArt.Image = .Art.Image
                PictureBoxTpa.Image = .Art.Stp.Tpa.Image
                TextBoxArtNom.Text = .Art.Nom_ESP
                ComboBoxRep.SelectedValue = .RepCom.Rep.Id
                TextBoxCom.Text = .RepCom.ComisioPercent
            End With
            mAllowEvents = True
        End Set
    End Property

    Private Sub LoadReps()
        Dim SQL As String = "SELECT CLI, ABR FROM (SELECT 0 AS CLI, '(seleccionar rep)' AS ABR UNION (SELECT CLI,ABR FROM CLIREP WHERE HASTA IS NULL)) DERIVEDTABLE ORDER BY ABR"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxRep
            .DataSource = oDs.Tables(0)
            .ValueMember = "CLI"
            .DisplayMember = "ABR"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mLineItmPnc
            .RepCom = New RepCom(Rep.FromNum(.Art.Emp, ComboBoxRep.SelectedValue), TextBoxCom.Text)
        End With

        RaiseEvent AfterUpdate(mLineItmPnc, New System.EventArgs)
        Me.Close()
    End Sub


    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    ComboBoxRep.SelectedIndexChanged, _
     TextBoxCom.TextChanged

        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class