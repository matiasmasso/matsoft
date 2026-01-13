Imports System.Globalization

Public Class ExcelHelper

    Public Enum HeaderRowStyles
        [Default]
        ElCorteIngles
    End Enum

    Public Shared Function getExcelFileFromStream(exs As List(Of Exception), ByVal oByte() As Byte, Optional ByVal sFilename As String = "") As String
        If sFilename = "" Then
            sFilename = System.Guid.NewGuid.ToString & ".xlsx"
            Dim sTmpFolder As String = MatHelperStd.FileSystemHelper.TmpFolder & "\"
            sFilename = System.IO.Path.Combine(sTmpFolder, sFilename)
        End If

        MatHelperStd.IOHelper.SaveStream(oByte, exs, sFilename)
        Return sFilename
    End Function


    Public Class Book
        Public Property Sheets As List(Of Sheet)
        Property Filename As String

        Public Enum UrlCods
            notSet
            customerDeliveries
        End Enum

        Public Sub New(Optional sFilename As String = "")
            _sheets = New List(Of Sheet)
            _filename = sFilename
        End Sub
    End Class

    Public Class Sheet
        Public Property Name As String
        Public Property Filename As String
        Public Property Rows As List(Of Row)
        Public Property Columns As List(Of Column)
        Public Property DisplayTotals As Boolean

        Public Property HeaderRowStyle As HeaderRowStyles

        Public Property ColumnHeadersOnFirstRow As Boolean

        Private _CultureInfo As Globalization.CultureInfo

        Public Enum NumberFormats
            NotSet
            PlainText
            [DDMMYY]
            [Integer]
            [Decimal2Digits]
            Euro
            W50
            Percent
            Kg
            KgD1
            m3
            m3D2
            mm
            CenteredText
        End Enum

        Public Property CultureInfo As CultureInfo
            Get
                Return _CultureInfo
            End Get
            Set(value As CultureInfo)
                _CultureInfo = value
                For Each oRow In _Rows
                    oRow.CultureInfo = _CultureInfo
                Next
            End Set
        End Property

        Public Sub New(Optional sName As String = "", Optional sFilename As String = "")
            MyBase.New()
            _Rows = New List(Of Row)
            _Columns = New List(Of Column)
            _Name = sName
            _Filename = sFilename
            _CultureInfo = CultureInfo.GetCultureInfo("es-ES")
            _HeaderRowStyle = HeaderRowStyles.Default
        End Sub

        Public Function Clon(Optional iRowFrom As Integer = 0, Optional iRowTo As Integer = 0) As Sheet
            Dim retval As New Sheet(Me.name, Me.filename)
            If iRowTo = 0 Then iRowTo = _rows.Count - 1
            If iRowTo >= _rows.Count Then iRowTo = _rows.Count - 1
            retval.columns = _columns
            For i As Integer = iRowFrom To iRowTo
                retval.rows.Add(_rows(i))
            Next
            Return retval
        End Function

        Shared Function Factory(ByVal oDs As System.Data.DataSet) As Sheet
            Dim retval As New Sheet
            Dim oTb As System.Data.DataTable = oDs.Tables(0)

            Dim i As Integer = 1
            Dim j As Integer

            For j = 0 To oTb.Columns.Count - 1
                Try
                    retval.addColumn(oTb.Columns(j).Caption)
                Catch ex As Exception
                End Try
            Next

            For Each oDataRow As System.Data.DataRow In oTb.Rows
                Dim oRow As Row = retval.addRow
                For j = 0 To oTb.Columns.Count - 1
                    Try
                        oRow.AddCell(oDataRow(j))
                    Catch ex As Exception
                    End Try
                Next
            Next

            Return retval
        End Function

        Public Function AddRow() As Row
            Dim retval As New Row(Me)
            retval.cultureInfo = _CultureInfo
            Me.Rows.Add(retval)
            Return retval
        End Function

        Public Function AddColumn(Optional sHeader As String = "", Optional oNumberFormat As NumberFormats = NumberFormats.NotSet) As Column
            Dim oColumn As New Column(sHeader, oNumberFormat)
            _Columns.Add(oColumn)
            Return oColumn
        End Function

        Public Function AddRowWithEmptyCells(cellsCount As Integer) As Row
            Dim retval = addRow()
            For i As Integer = 0 To cellsCount - 1
                retval.addCell()
            Next
            Return retval
        End Function

        Public Function AddRowWithCells(ByVal ParamArray CellTexts() As String) As Row
            Dim retval = AddRow()
            For Each sCellText As String In CellTexts
                retval.addCell(sCellText)
            Next
            Return retval
        End Function

        Public Function MatchHeaderCaptions(ParamArray Captions() As String) As Boolean
            Dim retval As Boolean
            If _Rows.Count > 0 Then
                Dim oFirstRow = _Rows(0)
                If Captions.Count <= oFirstRow.cells.Count Then
                    retval = True
                    For i = 0 To Captions.Count - 1
                        If oFirstRow.getString(i).Trim <> Captions(i).Trim Then
                            retval = False
                            Exit For
                        End If
                    Next
                End If
            End If
            Return retval
        End Function

        Public Sub TrimCols(maxCols As Integer)
            For Each oRow In _Rows
                For Col = oRow.cells.Count - 1 To maxCols - 1 Step -1
                    oRow.cells.RemoveAt(Col)
                Next
            Next
        End Sub

    End Class


    Public Class Column
        Public Property Header As String
        Public Property NumberFormat As Sheet.NumberFormats


        Public Sub New(sHeader As String, oNumberFormat As Sheet.NumberFormats)
            MyBase.New()
            _Header = sHeader
            _NumberFormat = oNumberFormat
        End Sub

        Public Shadows Function ToString() As String
            Dim retval As String = ""
            If String.IsNullOrEmpty(_Header) Then
                retval = MyBase.ToString()
            Else
                retval = _Header
            End If
            Return retval
        End Function
    End Class


    Public Class Row
        Public Property Sheet As Sheet
        Public Property Cells As List(Of Cell)
        Public Property CultureInfo As CultureInfo


        Public Sub New(oSheet As Sheet)
            MyBase.New()
            _sheet = oSheet
            _cells = New List(Of Cell)
        End Sub

        Public Function AddCell(Optional sContent As Object = Nothing, Optional sUrl As String = "", Optional oNumberFormat As Sheet.NumberFormats = Sheet.NumberFormats.NotSet) As Cell
            Dim oCell As New Cell(sContent, sUrl, oNumberFormat)
            _Cells.Add(oCell)
            Return oCell
        End Function

        Public Function AddCell(DtFch As Date) As Cell
            Dim oCell As New Cell
            If DtFch <> Nothing Then
                oCell = New Cell(DtFch, , Sheet.NumberFormats.DDMMYY)
            End If
            _Cells.Add(oCell)
            Return oCell
        End Function

        Public Function AddFormula(sFormulaR1C1 As String) As Cell
            Dim oCell As New Cell
            oCell.formulaR1C1 = sFormulaR1C1
            _Cells.Add(oCell)
            Return oCell
        End Function

        Public Function CellIdx(ColHeader As String) As Integer
            Dim retval = -1
            For idx As Integer = 0 To Sheet.Columns.Count - 1
                If Sheet.Columns(idx).Header.ToLower = ColHeader.ToLower Then
                    retval = idx
                End If
            Next
            Return retval
        End Function

        Public Function CellString(ColHeader As String) As String
            Return GetString(CellIdx(ColHeader))
        End Function
        Public Function CellGuid(ColHeader As String) As Guid?
            Return GetGuid(CellIdx(ColHeader))
        End Function
        Public Function CellInt(ColHeader As String) As Integer
            Return GetInt(CellIdx(ColHeader))
        End Function
        Public Function CellDecimal(ColHeader As String) As Decimal
            Return GetDecimal(CellIdx(ColHeader))
        End Function
        Public Function FchSpain(ColHeader As String) As Date
            Return GetFchSpain(CellIdx(ColHeader))
        End Function

        Public Function GetString(CellIdx As Integer) As String
            Dim retval As String = ""
            If CellIdx >= 0 And CellIdx < _Cells.Count Then
                retval = _Cells(CellIdx).Content
            End If
            Return retval
        End Function

        Public Function GetGuid(CellIdx As Integer) As Guid?
            Dim retval As Guid? = Nothing
            Dim src = GetString(CellIdx)
            Dim guidCandidate As Guid
            If Guid.TryParse(src, guidCandidate) Then
                retval = guidCandidate
            End If
            Return retval
        End Function


        Public Function GetInt(CellIdx As Integer) As Integer
            Dim retval As Integer = 0
            Dim src = GetString(CellIdx)
            If TextHelper.VbIsNumeric(src) Then retval = Decimal.Parse(src, _CultureInfo)
            Return retval
        End Function

        Public Function GetDecimal(CellIdx As Integer) As Decimal
            Dim retval As Decimal = 0
            Dim src = GetString(CellIdx)
            If TextHelper.VbIsNumeric(src) Then
                retval = Decimal.Parse(src, _CultureInfo)
            End If
            Return retval
        End Function

        Public Function GetFchSpain(CellIdx As Integer) As Date
            Dim src = GetString(CellIdx)
            Dim retval As Date = MatHelperStd.TimeHelper.ParseFchSpain(src)
            Return retval.Date
        End Function
    End Class


    Public Class Cell
        Property Url As String
        Property Content As Object
        Property FormulaR1C1 As String
        Property Indent As Integer
        Property Alignment As Alignments
        Property CellStyle As CellStyles



        Private _NumberFormat As Sheet.NumberFormats

        Public Enum Alignments
            NotSet
            Left
            Center
            Right
        End Enum

        Public Enum CellStyles
            NotSet
            Bold
            Italic
            Total
        End Enum

        Public Sub New(Optional oContent As Object = Nothing, Optional sUrl As String = "", Optional oNumberFormat As Sheet.NumberFormats = Sheet.NumberFormats.NotSet)
            MyBase.New()
            _Content = oContent
            If sUrl > "" Then _Url = sUrl
        End Sub

        Public Function IsEmpty() As Boolean
            Dim retval As Boolean
            If _Content Is Nothing Then
                retval = True
            ElseIf TypeOf (_Content) Is String Then
                retval = _Content.ToString.Trim = ""
            ElseIf TypeOf (_Content) Is Double Then
                retval = _Content = 0
            End If
            Return retval
        End Function

        Public Function IsNotEmpty() As Boolean
            Return Not IsEmpty()
        End Function

        Public Shared Widening Operator CType(v As Cell) As String
            Throw New NotImplementedException()
        End Operator

        Public Shadows Function ToString() As String
            Dim retval As String = ""
            If String.IsNullOrEmpty(_Content) Then
                retval = MyBase.ToString()
            Else
                retval = _Content
            End If
            Return retval
        End Function
    End Class


    Public Class Cells
        Inherits List(Of Cell)
    End Class


    Public Class ValidationResult
        Property Row As Integer
        Property Text As String

        Property Cod As Cods

        Public Enum Cods
            Success
            Fail
        End Enum

        Shared Function Factory(row As Integer, cod As Cods, text As String) As ValidationResult
            Dim retval As New ValidationResult
            With retval
                .row = row
                .cod = cod
                .text = text
            End With
            Return retval
        End Function
    End Class
End Class
