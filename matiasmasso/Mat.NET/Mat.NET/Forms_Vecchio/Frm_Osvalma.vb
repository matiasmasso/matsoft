
Imports system.xml

Public Class Frm_Osvalma
    Private mDoc As XmlDocument
    Private mEmp as DTOEmp = BLL.BLLApp.Emp

    Private Enum Cols
        Alb
        Fch
        Exp
        Plaza
        Clave
        Destino
        Servicio
        Producto
        Bultos
        Kg
        Eur
        IO
    End Enum

    Public WriteOnly Property XmlDoc() As XmlDocument
        Set(ByVal value As XmlDocument)
            mDoc = value
            refresca()
        End Set
    End Property

    Private Sub refresca()
        Dim oFraNode As XmlNode = mDoc.DocumentElement.SelectSingleNode("/factura")

        TextBoxFra.Text = oFraNode.Attributes("numero").Value
        TextBoxFch.Text = oFraNode.Attributes("fecha").Value
        'TextBoxAmt.Text = mDoc.DocumentElement.SelectSingleNode("/factura/resumen").Attributes("total").Value

        Dim sNif As String = mDoc.DocumentElement.SelectSingleNode("/factura/emisor/nif").InnerText.Trim
        Dim oPrv As Contact = App.Current.emp.GetContactByNIF(sNif)
        PictureBoxLogo.Image = oPrv.Img48

        Dim oTb As DataTable = CreateTable()
        Dim oRow As DataRow
        Dim oNode As XmlNode
        Dim sAlb As String
        For Each oNode In mDoc.DocumentElement.SelectNodes("/factura/detalle/linea")
            oRow = oTb.NewRow
            Try
                sAlb = oNode.Attributes("referencia").Value
                oRow(Cols.Alb) = IIf(IsNumeric(sAlb), sAlb, 0)
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Fch) = oNode.Attributes("fecha").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Exp) = oNode.Attributes("expedicion").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Plaza) = oNode.Attributes("origen").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Clave) = oNode.Attributes("clave").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Destino) = oNode.Attributes("remitente").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Servicio) = oNode.Attributes("servicio").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Producto) = oNode.Attributes("producto").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Bultos) = oNode.Attributes("bultos").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Kg) = oNode.Attributes("kilos").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.Eur) = oNode.Attributes("importe").Value
            Catch ex As Exception
            End Try
            Try
                oRow(Cols.IO) = IIf(oNode.Attributes("tipo").Value = "entrega", 1, 2)
            Catch ex As Exception
            End Try
            oTb.Rows.Add(oRow)
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Alb)
                .HeaderText = "albará"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Exp)
                .HeaderText = "expedicio"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Plaza)
                .HeaderText = "plaça"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Clave)
                .HeaderText = "clau"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 20
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(Cols.Destino)
                .HeaderText = "destinació"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With
    End Sub

    Private Function CreateTable() As DataTable
        Dim oTb As New DataTable
        With oTb
            .Columns.Add("ALB", System.Type.GetType("System.Int32"))
            .Columns.Add("FCH", System.Type.GetType("System.DateTime"))
            .Columns.Add("EXP", System.Type.GetType("System.String"))
            .Columns.Add("PLZ", System.Type.GetType("System.String"))
            .Columns.Add("KEY", System.Type.GetType("System.String"))
            .Columns.Add("DST", System.Type.GetType("System.String"))
            .Columns.Add("SRV", System.Type.GetType("System.Int32"))
            .Columns.Add("PRD", System.Type.GetType("System.Int32"))
            .Columns.Add("BTS", System.Type.GetType("System.Int32"))
            .Columns.Add("KGS", System.Type.GetType("System.Int32"))
            .Columns.Add("EUR", System.Type.GetType("System.Decimal"))
            .Columns.Add("IO", System.Type.GetType("System.Int32"))
        End With
        Return oTb
    End Function
End Class