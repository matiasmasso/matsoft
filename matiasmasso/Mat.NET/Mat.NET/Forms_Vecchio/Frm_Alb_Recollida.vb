

Public Class Frm_Alb_Recollida
    Private mRecollida As Recollida

    Private Enum Cols
        ArtGuid
        Qty
        Nom
    End Enum

    Public Sub New(oRecollida As Recollida)
        MyBase.New()
        Me.InitializeComponent()
        mRecollida = oRecollida
        refresca()
    End Sub

    Private Sub refresca()
        LoadEstats()
        LoadCarrecs()
        LoadAccions()

        With mRecollida
            Me.Text = "recollida albará " & .Alb.Id
            DateTimePicker1.Value = .Fch

            TextBoxOrigenNom.Text = .OrigenNom
            TextBoxOrigenAdr.Text = .OrigenAdr
            Xl_CitOrigen.Zip = .OrigenZip
            TextBoxOrigenTel.Text = .OrigenTel
            TextBoxOrigenContact.Text = .OrigenContact
            TextBoxBultos.Text = .Bultos
            TextBoxMotiu.Text = .Motiu

            TextBoxDestiNom.Text = .DestiNom
            TextBoxDestiAdr.Text = .DestiAdr
            Xl_CitDesti.Zip = .DestiZip
            TextBoxDestiTel.Text = .DestiTel
            TextBoxDestiContact.Text = .DestiContact
        End With
        LoadGrid()
        ButtonOk.Enabled = True
    End Sub

    Private Sub LoadGrid()
        Dim oTb As New DataTable
        oTb.Columns.Add(New DataColumn("ArtGuid", System.Type.GetType("System.Guid")))
        oTb.Columns.Add(New DataColumn("Qty", System.Type.GetType("System.Int32")))
        oTb.Columns.Add(New DataColumn("Nom", System.Type.GetType("System.String")))

        For Each oItem As QtyArt In mRecollida.Items
            Dim oRow As DataRow = oTb.NewRow
            oRow(Cols.ArtGuid) = oItem.Art.Guid
            oRow(Cols.Qty) = oItem.Qty
            oRow(Cols.Nom) = oItem.Art.Nom_ESP
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .AllowUserToDeleteRows = True
            .DataSource = oTb
            '.SelectionMode = DataGridViewSelectionMode.CellSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .AllowUserToResizeRows = False
            .ReadOnly = False


            With .Columns(Cols.ArtGuid)
                .Visible = False
            End With
            With .Columns(Cols.Qty)
                .HeaderText = "Quantitat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.ReadOnly = True
            End With
        End With
    End Sub

    Private Function GetItemsFromGrid() As QtyArts
        Dim oItems As New QtyArts
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim iQty As Integer = oRow.Cells(Cols.Qty).Value
            Dim oGuid As Guid = oRow.Cells(Cols.ArtGuid).Value
            Dim oArt As New Art(oGuid)
            Dim oItem As New QtyArt(iQty, oArt)
            oItems.Add(oItem)
        Next
        Return oItems
    End Function

    Private Sub LoadEstats()
        UIHelper.LoadComboFromEnum(ComboBoxEstat, GetType(Recollida.EstatsDeLaMercancia))
    End Sub

    Private Sub LoadCarrecs()
        UIHelper.LoadComboFromEnum(ComboBoxCarrec, GetType(Recollida.Carrecs))
    End Sub

    Private Sub LoadAccions()
        UIHelper.LoadComboFromEnum(ComboBoxAccio, GetType(Recollida.Accions))
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        With mRecollida
            .Fch = DateTimePicker1.Value

            .OrigenNom = TextBoxOrigenNom.Text
            .OrigenAdr = TextBoxOrigenAdr.Text
            .OrigenZip = Xl_CitOrigen.Zip
            .OrigenTel = TextBoxOrigenTel.Text
            .OrigenContact = TextBoxOrigenContact.Text
            .Bultos = TextBoxBultos.Text
            .EstatDeLaMercancia = ComboBoxEstat.SelectedIndex
            .Motiu = TextBoxMotiu.Text

            .DestiNom = TextBoxDestiNom.Text
            .DestiAdr = TextBoxDestiAdr.Text
            .DestiZip = Xl_CitDesti.Zip
            .DestiTel = TextBoxDestiTel.Text
            .DestiContact = TextBoxDestiContact.Text
            .Carrec = ComboBoxCarrec.SelectedIndex
            .Accio = ComboBoxAccio.SelectedIndex

            .Items = GetItemsFromGrid()

            .Update()
        End With
        MailMessage()
        Me.Close()
    End Sub

    Private Function GetHtmlTableFromItems(oitems As QtyArts) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("<table>")
        sb.Append("<tr><td align='right'>quantitat</td><td align='left'>article</td></tr>")
        For Each oItem As QtyArt In oitems
            sb.Append("<tr>")
            sb.Append("<td>")
            sb.Append(oItem.Qty.ToString)
            sb.Append("</td>")
            sb.Append("<td>")
            sb.Append(oItem.Art.Nom_ESP)
            sb.Append("</td>")
            sb.Append("</tr>")
        Next
        sb.Append("</table>")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Private Sub MailMessage()
        Dim oMgz As Mgz = mRecollida.Alb.Mgz
        Dim oLang As DTOLang = oMgz.Lang
        Dim sTo As String = oMgz.Email

        Dim sBody As String = (New Txt(Txt.Ids.RecollidaMagatzem)).ToHtml( _
            mRecollida.Alb.Mgz.Lang, _
            mRecollida.OrigenNom, _
            mRecollida.OrigenAdr, _
            mRecollida.OrigenZip.ZipyCityZon, _
        mRecollida.Bultos, _
        mRecollida.EstatDeLaMercancia.ToString, _
        mRecollida.Motiu, _
        mRecollida.DestiNom, _
        mRecollida.DestiAdr, _
        mRecollida.DestiZip.ZipyCityZon, _
        mRecollida.DestiTel, _
        mRecollida.DestiContact, _
        mRecollida.Carrec.ToString, _
        mRecollida.Accio.ToString, _
        GetHtmlTableFromItems(mRecollida.Items))

        Dim sSubject As String = oLang.Tradueix("Recogida", "Recullida", "Callback") & " " & mRecollida.Id & ""
        'Dim oArrayList As New ArrayList
        'oArrayList.Add(sFilename)
        Dim exs as New List(Of exception)
        If Not MatOutlook.NewMessage(sTo, "", , sSubject, , sBody, , exs) Then
            UIHelper.WarnError( exs, "error al redactar el missatge")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class