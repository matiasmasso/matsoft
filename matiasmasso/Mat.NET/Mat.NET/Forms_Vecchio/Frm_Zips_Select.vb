

Public Class Frm_Zips_Select

    Private mLoaded As Boolean
    Private mIdx As Integer
    Private mZips As Zips
    Private mZip As Zip
    Private mDs As DataSet
    Private mCountries As Countries

    Private Enum Cols
        Guid
        IsoPais
        Flag
        ZipNom
    End Enum

    Public ReadOnly Property Zip() As Zip
        Get
            Return mZip
        End Get
    End Property

    Public WriteOnly Property Zips() As Zips
        Set(ByVal Value As Zips)
            mZips = Value
            mDs = CreateDataSource()
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        Dim oZip As Zip
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim oCountry As Country
        Dim oDefaultCountry As Country = MaxiSrvr.Country.Default
        mCountries = New Countries
        For Each oZip In mZips
            oCountry = oZip.Location.Zona.Country
            oRow = oTb.NewRow
            oRow(Cols.Guid) = oZip.Guid
            oRow(Cols.ZipNom) = oZip.ZipyCityZon
            oRow(Cols.IsoPais) = oCountry.ISO

            Dim oImg As Image = My.Resources.empty
            If oCountry.IsDefault Then
                oRow(Cols.Flag) = maxisrvr.GetByteArrayFromImg(My.Resources.empty)
            Else
                oImg = oCountry.Flag
                If oImg Is Nothing Then
                    Dim oBuffer As Byte() = maxisrvr.GetByteArrayFromImg(My.Resources.empty)
                    oRow(Cols.Flag) = oBuffer
                Else
                    'Dim MemStream As New IO.MemoryStream
                    'oImg.Save(MemStream, oImg.RawFormat)
                    oRow(Cols.Flag) = maxisrvr.GetByteArrayFromImg(oImg)
                End If
            End If


            oTb.Rows.Add(oRow)
            AddToCountries(oCountry)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.IsoPais)
                .Visible = False
            End With
            With .Columns(Cols.Flag)
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.ZipNom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

    End Sub

    Private Sub AddToCountries(ByVal oCountry As Country)
        If oCountry.ISO = "ES" Then Exit Sub
        For Each nCountry As Country In mCountries
            If nCountry.ISO = oCountry.ISO Then Exit Sub
        Next
        mCountries.Add(oCountry)
    End Sub


    Private Function CreateDataSource() As DataSet
        Dim oTb As New DataTable
        With oTb.Columns
            .Add(New DataColumn("Guid", System.Type.GetType("System.Guid")))
            .Add(New DataColumn("PaisId", System.Type.GetType("System.String")))
            .Add(New DataColumn("Flag", System.Type.GetType("System.Byte[]")))
            .Add(New DataColumn("ZipNom", System.Type.GetType("System.String")))
        End With

        Dim oDs As New DataSet
        oDs.Tables.Add(oTb)
        Return oDs
    End Function

    Private Function CurrentZip() As Zip
        Dim oZip As Zip = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = GuidHelper.GetGuid(oRow.Cells(Cols.Guid).Value)
            oZip = New Zip(oGuid)
        End If
        Return oZip
    End Function


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Me.Close()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.Close()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        mZip = CurrentZip()
    End Sub
End Class
