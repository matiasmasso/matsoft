

Public Class Frm_Atlas
    Private mDsPaisos As DataSet
    Private mDsZonas As DataSet
    Private mDsLocations As DataSet
    Private mDsZips As DataSet
    Private mDsContacts As DataSet
    Private mEmp As DTO.DTOEmp =BLL.BLLApp.Emp
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Nom
    End Enum

    Private Sub Frm_Atlas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadPaisos()
        mAllowEvents = True
    End Sub

    Private Sub LoadPaisos()
        Dim SQL As String = "SELECT P.ISO, P.Nom_CAT as Nom " _
        & "FROM Country P INNER JOIN " _
        & "CIT ON CIT.ISOPAIS LIKE P.ISO INNER JOIN " _
        & "CliAdr ON CIT.Id = CliAdr.CitNum " _
        & "GROUP BY P.ISO, P.Nom_CAT " _
        & "ORDER BY P.Nom_CAT"
        mDsPaisos = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)

        With ComboBoxPaisos
            .ValueMember = "Id"
            .DisplayMember = "Nom"
            .DataSource = mDsPaisos.Tables(0)
        End With
    End Sub

    Private Sub LoadZonas()
        Dim SQL As String = "SELECT Zona.Guid, Zona.Nom " _
        & "FROM Zona INNER JOIN " _
        & "Location ON Location.Zona = Zona.Guid INNER JOIN " _
        & "Zip ON Zip.Location=Location.Guid INNER JOIN " _
        & "CliAdr ON ZIP.Guid = CliAdr.Zip " _
        & "WHERE CLIADR.EMP=@Emp AND " _
        & "Zona.Country=@Country " _
        & "GROUP BY Zona.Guid, Zona.Nom " _
        & "ORDER BY Zona.Nom"
        mDsZonas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Emp", mEmp.Id, "@Country", CurrentCountry.Guid.ToString)

        With ComboBoxZonas
            .ValueMember = "Guid"
            .DisplayMember = "Nom"
            .DataSource = mDsZonas.Tables(0)
        End With
    End Sub

    Private Sub LoadLocations()
        Dim SQL As String = "SELECT Location.Guid, Location.Nom " _
        & "FROM Location INNER JOIN " _
        & "Zip ON Zip.Location=Location.Guid INNER JOIN " _
        & "CliAdr ON ZIP.Guid = CliAdr.Zip " _
        & "WHERE CLIADR.EMP=@Emp AND " _
        & "Location.Zona=@Zona " _
        & "GROUP BY Location.Guid, Location.Nom " _
        & "ORDER BY Location.Nom"
        mDsLocations = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Emp", mEmp.Id, "@Zona", CurrentZona.Guid.ToString)

        With ComboBoxZonas
            .ValueMember = "Guid"
            .DisplayMember = "Nom"
            .DataSource = mDsLocations.Tables(0)
        End With
    End Sub

    Private Sub LoadZips()
        Dim SQL As String = "SELECT Zip.Guid, Zip.ZipCod " _
        & "FROM Zip INNER JOIN " _
        & "CliAdr ON ZIP.Guid = CliAdr.Zip " _
        & "WHERE CLIADR.EMP=@Emp AND " _
        & "Zip.Location=@Location " _
        & "GROUP BY Zip.Guid, Zip.ZipCod " _
        & "ORDER BY Zip.ZipCod"
        mDsZips = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@Emp", mEmp.Id, "@Location", CurrentLocation.Guid.ToString)

        With ComboBoxZips
            .ValueMember = "Nom"
            .DisplayMember = "Nom"
            .DataSource = mDsZips.Tables(0)
        End With
    End Sub

    Private Sub LoadContacts()
        Dim SQL As String = "SELECT CLIGRAL.Cli AS Id, RaoSocial as Nom " _
        & "FROM CLIGRAL INNER JOIN " _
        & "CLIADR ON CLIGRAL.EMP=CLIADR.EMP AND CLIGRAL.CLI=CLIADR.CLI inner JOIN " _
        & "CIT ON CIT.CITNUM=CLIADR.CIT " _
        & "WHERE CLIADR.EMP=" & mEmp.Id & " AND " _
        & "CIT.CIT LIKE '" & maxisrvr.SQLfriendly(ComboBoxZips.Text) & "' " _
        & "GROUP BY CLIGRAL.Cli, RaoSocial " _
        & "ORDER BY RaoSocial"
        mDsContacts = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsContacts.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentCountry() As Country
        Dim sPais As String = ComboBoxPaisos.SelectedValue
        Dim oCountry As New Country(sPais)
        Return oCountry
    End Function

    Private Function CurrentZona() As MaxiSrvr.Zona
        Dim oGuid As Guid = ComboBoxZonas.SelectedValue
        Dim retval As New MaxiSrvr.Zona(oGuid)
        Return retval
    End Function

    Private Function CurrentLocation() As MaxiSrvr.Location
        Dim oGuid As Guid = ComboBoxLocations.SelectedValue
        Dim retval As New MaxiSrvr.Location(oGuid)
        Return retval
    End Function

    Private Function CurrentZip() As MaxiSrvr.Zip
        Dim oGuid As Guid = ComboBoxZips.SelectedValue
        Dim retval As New MaxiSrvr.Zip(oGuid)
        Return retval
    End Function

    Private Function CurrentContact() As MaxiSrvr.Contact
        Dim oContact As Contact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = DataGridView1.CurrentRow.Cells(Cols.Id).Value
            oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, LngId)
        End If
        Return oContact
    End Function

    Private Sub ComboBoxPaisos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxPaisos.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            LoadZonas()
            mAllowEvents = True
        End If
    End Sub

    Private Sub ComboBoxZonas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxZonas.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            LoadLocations()
            mAllowEvents = True
        End If
    End Sub

    Private Sub ComboBoxZips_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxZips.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            LoadContacts()
            mAllowEvents = True
        End If
    End Sub

    Private Sub ComboBoxLocations_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ComboBoxLocations.SelectedIndexChanged
        If mAllowEvents Then
            mAllowEvents = False
            LoadZips()
            mAllowEvents = True
        End If

    End Sub
End Class