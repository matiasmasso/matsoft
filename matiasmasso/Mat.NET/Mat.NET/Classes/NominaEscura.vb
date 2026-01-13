Imports Microsoft.Office.Interop
Imports System.Collections.Generic

Public Class NominasEscuraFile
    Private _Word As Word.Application
    Private _Document As Word.Document = Nothing
    Private _Fch As Date
    Private _Pages As NominasEscura

    Public Event ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs)

    Public Sub New(DtFch As Date)
        MyBase.New()
        _Fch = DtFch
    End Sub

    Public Function Open(sFilename As String, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            _Word = New Word.Application
            _Word.Visible = False
            _Document = _Word.Documents.Open(sFilename)
            retval = True

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public ReadOnly Property Pages As NominasEscura
        Get
            If _Pages Is Nothing Then
                _Pages = New NominasEscura
                Dim iPagesCount As Integer = _Document.Range.Information(Word.WdInformation.wdNumberOfPagesInDocument)
                For idx As Integer = 1 To iPagesCount
                    Dim oPage As New NominaEscura(_Document, idx, _Fch)
                    _Pages.Add(oPage)
                Next
            End If
            Return _Pages
        End Get
    End Property

    Public Sub Close()
        If _Document IsNot Nothing Then
            _Document.Close(Word.WdSaveOptions.wdDoNotSaveChanges)
        End If
        _Word = Nothing
    End Sub

End Class

Public Class NominaEscura
    Private _Document As Word.Document = Nothing
    Private _PageIndex As Integer

    Private _ShapeRange As Word.ShapeRange
    Private _TextShapes As List(Of String)

    Public Property Fch As Date
    Public Property Name As String
    Public Property Address As String
    Public Property Location As String
    Public Property Provincia As String
    Public Property Zip As String
    Public Property Categoria As String
    Public Property Antiguedad As String
    Public Property NIF As String
    Public Property NumSeguretatSocial As String
    Public Property Tarifa As String
    Public Property CodCT As String
    Public Property Seccion As String
    Public Property Numero As String
    Public Property Periodo As String
    Public Property Dias As String
    Public Property Items As NominaEscuraItems
    Public Property TotalRemuneracio As String
    Public Property ProrrataPagasExtras As String
    Public Property BaseSeguretatSocial As String
    Public Property BaseATyDesempleo As String
    Public Property BaseIRPF As String
    Public Property TotalDevengat As String
    Public Property TotalDeduccions As String
    Public Property Liquid As String


    Public ReadOnly Property ShapeRange As Word.ShapeRange
        Get
            If _ShapeRange Is Nothing Then
                Dim oSelection As Word.Selection = _Document.Application.Selection
                Dim oPageRange As Word.Range
                oPageRange = oSelection.GoTo(What:=Word.WdGoToItem.wdGoToPage, Which:=Word.WdGoToDirection.wdGoToAbsolute, Count:=_PageIndex)
                oPageRange.End = oSelection.Bookmarks("\Page").Range.End
                _ShapeRange = oPageRange.ShapeRange
            End If
            Return _ShapeRange
        End Get
    End Property

    Public Sub New(oDocument As Word.Document, iPageIndex As Integer, DtFch As Date)
        MyBase.New()

        _Document = oDocument
        _PageIndex = iPageIndex
        _Fch = DtFch

        _Name = GetText(1, 38, 45)
        _Address = GetText(2, 38, 45)
        _Location = GetText(3, 38, 45)
        _Provincia = GetText(4, 38, 45)
        _Zip = GetText(5, 71, 10)
        _Categoria = GetText(15, 38, 12)
        _Antiguedad = GetText(15, 64, 9)
        _NIF = GetText(15, 75, 13)
        _NumSeguretatSocial = GetText(23, 4, 15)
        _Tarifa = GetText(23, 22, 2)
        _CodCT = GetText(23, 26, 3)
        _Seccion = GetText(23, 31, 5)
        _Numero = GetText(23, 42, 2)
        _Periodo = GetText(23, 46, 36)
        _Dias = GetText(23, 82, 5)

        _Items = New NominaEscuraItems
        For i As Integer = 29 To 48
            Dim sCod As String = GetText(i, 26, 3)
            Dim sConcepte As String = GetText(i, 32, 30)
            If IsNumeric(sCod) Then
                Dim oCod As New DTONominaConcepte(sCod, sConcepte)
                Dim sQty As String = GetText(i, 6, 6)
                Dim sPrice As String = GetText(i, 12, 10)
                Dim sDevengo As String = GetText(i, 67, 14)
                Dim sDeduccio As String = GetText(i, 81, 14)
                Dim oItem As New NominaEscuraItem(oCod)
                With oItem
                    If IsNumeric(sQty) Then .Qty = CInt(sQty)
                    If IsNumeric(sPrice) Then .Price = CDec(sPrice)
                    If IsNumeric(sDevengo) Then .Devengo = CDec(sDevengo)
                    If IsNumeric(sDeduccio) Then .Deduccio = CDec(sDeduccio)
                End With
                _Items.Add(oItem)
            Else
                Exit For
            End If
        Next

        _TotalRemuneracio = GetText(56, 0, 14)
        _ProrrataPagasExtras = GetText(56, 14, 13)
        _BaseSeguretatSocial = GetText(56, 27, 13)
        _BaseATyDesempleo = GetText(56, 40, 16)
        _BaseIRPF = GetText(56, 56, 14)
        _TotalDevengat = GetText(56, 69, 12)
        _TotalDeduccions = GetText(56, 81, 14)
        _Liquid = GetText(66, 69, 14)
    End Sub

    Public ReadOnly Property TextShapes As List(Of String)
        Get
            If _TextShapes Is Nothing Then
                _TextShapes = New List(Of String)
                For Each oShape As Word.Shape In Me.ShapeRange
                    Dim oTextFrame As Word.TextFrame = oShape.TextFrame
                    If oTextFrame.HasText Then
                        Dim oTextRange As Word.Range = oTextFrame.TextRange
                        _TextShapes.Add(oTextRange.Text)
                    End If
                Next
            End If
            Return _TextShapes
        End Get
    End Property

    Private Function GetText(iShapeIndex As Integer, iStartChar As Integer, iLen As Integer) As String
        Dim retval As String = ""
        If Me.TextShapes.Count > iShapeIndex Then
            Dim sTextShape As String = _TextShapes(iShapeIndex)
            If sTextShape.Length > iStartChar Then
                If sTextShape.Length > iStartChar + iLen Then
                    retval = sTextShape.Substring(iStartChar, iLen)
                Else
                    retval = sTextShape.Substring(iStartChar)
                End If
            End If
        End If
        Return retval.Trim
    End Function

    Public Function LoadNomina(ByRef exs As List(Of Exception)) As Nomina
        Dim oNomina As Nomina = ReadNomina(exs)
        Dim oDocFile As DTODocFile = Me.DocFile(_ShapeRange, oNomina.Staff, exs)
        oNomina.Cca.DocFile = oDocFile
        Return oNomina
    End Function

    Private Function DocFile(oShapeRange As Word.ShapeRange, oStaff As Staff, ByRef exs As List(Of Exception)) As DTODocFile
        Dim sFilename As String = SavePage(oShapeRange, oStaff)
        Dim retval As DTODocFile = BLL_DocFile.FromFile(sFilename, exs)
        Dim oThumb As Image = retval.Thumbnail
        Return retval
    End Function

    Private Function SavePage(oShapeRange As Word.ShapeRange, oStaff As Staff) As String
        Dim oPageCopy As Word.Document = Me.PageCopy
        InsertLogo(oPageCopy)
        InsertBank(oPageCopy, oStaff.Iban)

        Dim retval As String = BLL.FileSystemHelper.GetPdfTmpFileName()
        oPageCopy.SaveAs(retval, Word.WdSaveFormat.wdFormatPDF)
        oPageCopy.Close(Word.WdSaveOptions.wdDoNotSaveChanges)
        Return retval
    End Function

    Private Function PageCopy() As Word.Document
        Dim oShapeRange As Word.ShapeRange = Me.ShapeRange
        oShapeRange.Select()
        _Document.Application.Selection.Copy()
        Dim retval As Word.Document = _Document.Application.Documents.Add()
        retval.Range.Paste()
        Return retval
    End Function

    Private Sub InsertLogo(oDocument As Word.Document)
        Dim sLogoFileName As String = NominaEscura.LogoFilename
        Dim oRange As Word.Range = oDocument.Content
        'Dim ObjPic As Microsoft.Office.Interop.Word.InlineShape = oDocument.InlineShapes.AddPicture(sLogoFileName, , , oRange)
        Dim ObjPic2 As Microsoft.Office.Interop.Word.Shape = oDocument.Shapes.AddPicture(sLogoFileName, , , -40)
    End Sub

    Private Sub InsertBank(oDocument As Word.Document, oIban As DTOIban)
        If oIban IsNot Nothing Then
            Dim orientation As Microsoft.Office.Core.MsoTextOrientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal

            Dim ObjBnc As Microsoft.Office.Interop.Word.Shape = oDocument.Shapes.AddTextbox(orientation, 92, 704, 600, 20)
            ObjBnc.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse
            ObjBnc.TextFrame.TextRange.Text = BLL.BLLIban.BankNom(oIban)

            Dim ObjCta As Microsoft.Office.Interop.Word.Shape = oDocument.Shapes.AddTextbox(orientation, 92, 717, 600, 20)
            ObjCta.Line.Visible = Microsoft.Office.Core.MsoTriState.msoFalse
            ObjCta.TextFrame.TextRange.Text = BLL.BLLIban.Formated(oIban)
        End If
    End Sub

    Shared Function LogoFilename() As String
        Static Loaded As Boolean
        Dim retval As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\logo.gif"
        If Not Loaded Then
            Dim oLogo As Image = My.Resources.Logo_M_O_2cm_300x300
            Dim oThumb As Image = ImageHelper.GetThumbnail(oLogo, 100)
            oThumb.Save(retval)
            Loaded = True
        End If
        Return retval
    End Function

    Private Function ReadNomina(ByRef exs As List(Of Exception)) As Nomina
        Dim oEmp As DTOEmp = BLL.BLLApp.Emp

        Dim DcDietas As Decimal = 0
        Dim DcSegSoc As Decimal = 0
        Dim DcIrpf As Decimal = 0
        Dim DcEmbargos As Decimal = 0
        Dim DcDeutes As Decimal = 0

        Dim retval As New Nomina(oEmp, _NumSeguretatSocial, _Fch)
        For Each oEscuraItem As NominaEscuraItem In _Items
            Dim oItem As New NominaItem(oEscuraItem.Cod)
            Select Case oItem.Concepte.Id
                Case 602, 603, 604, 678 'dietas
                    DcDietas = oEscuraItem.Devengo
                Case 703
                    DcEmbargos += oEscuraItem.Deduccio
                Case 740
                    DcDeutes += oEscuraItem.Deduccio
                Case 995, 996, 997
                    DcSegSoc = DcSegSoc + oEscuraItem.Deduccio
                Case 999
                    DcIrpf = oEscuraItem.Deduccio
            End Select
            With oItem
                .Qty = oEscuraItem.Qty
                .Preu = New Amt(oEscuraItem.Price)
                .Concepte = oEscuraItem.Cod
            End With
            retval.Items.Add(oItem)
        Next

        With retval
            If .Staff Is Nothing Then
                exs.Add(New Exception("no hi ha registrat cap '" & _Name & "' amb el num.Seguretat Social " & _NumSeguretatSocial))
            Else
                If .Staff.Iban Is Nothing Then
                    exs.Add(New Exception(.Staff.Nom_o_NomAlias & " no té registrat cap compte corrent"))
                Else
                    .IbanDigits = .Staff.Iban.Digits
                End If
            End If

            If IsNumeric(_TotalDevengat) Then
                .Devengat = New Amt(CDec(_TotalDevengat))
            End If

            .Fch = _Fch

            .Dietes = New Amt(DcDietas)
            .Embargos = New Amt(DcEmbargos)
            .Deutes = New Amt(DcDeutes)
            .SegSocial = New Amt(DcSegSoc)
            .Irpf = New Amt(DcIrpf)

            If IsNumeric(Me.BaseIRPF) Then
                .IrpfBase = New Amt(CDec(Me.BaseIRPF))
            End If

            If IsNumeric(Me.Liquid) Then
                .Liquid = New Amt(CDec(Me.Liquid))
            Else
                .Liquid = New Amt()
            End If

        End With
        Return retval
    End Function

End Class

Public Class NominasEscura
    Inherits System.ComponentModel.BindingList(Of NominaEscura)
End Class

Public Class NominaEscuraItem
    Public Property Qty As Integer
    Public Property Price As Decimal
    Public Property Cod As DTONominaConcepte
    Public Property Devengo As Decimal
    Public Property Deduccio As Decimal

    Public Sub New(oCod As DTONominaConcepte)
        MyBase.New()
        _Cod = oCod
    End Sub
End Class

Public Class NominaEscuraItems
    Inherits System.ComponentModel.BindingList(Of NominaEscuraItem)
End Class

Public Class NominaEscuraCod
    Public Property Id As Integer
    Public Property Name As String
    Public Property Qualifier As String
End Class


