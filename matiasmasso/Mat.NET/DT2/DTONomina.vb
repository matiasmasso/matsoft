Imports System.Web.Script.Serialization

Public Class DTONomina
    Property Cca As DTOCca

    Public Property Staff As DTOStaff
    Public Property Devengat As DTOAmt
    Public Property Dietes As DTOAmt
    Public Property SegSocial As DTOAmt
    Public Property IrpfBase As DTOAmt
    Public Property Irpf As DTOAmt
    Public Property Embargos As DTOAmt
    Public Property Deutes As DTOAmt
    Public Property Liquid As DTOAmt
    Public Property IbanDigits As String
    Public Property Iban As DTOIban
    Public Property Items As List(Of Item)

    Public Property IsLoaded As Boolean
    Public Property IsNew As Boolean

    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oCca As DTOCca)
        MyBase.New
        _Cca = oCca
    End Sub

    Public Sub New(oStaff As DTOStaff)
        MyBase.New
        _Staff = oStaff
        _Iban = _Staff.Iban
    End Sub

    Shared Function CcaConcepte(oNomina As DTONomina) As String
        Dim sText As String = "nomina"
        Dim sNom As String = DTOStaff.AliasOrNom(oNomina.Staff)
        Dim iLenText As Integer = sText.Length
        Dim iLenNom As Integer = sNom.Length
        Dim iLenField As Integer = 60
        If iLenNom + iLenText + 1 > iLenField Then
            sNom = sNom.Substring(0, iLenField - iLenText - 1)
        End If
        Dim retval = String.Format("{0}-{1}", sNom, sText)
        Return retval
    End Function

    Public Class Item
        Property Concepte As Concepte
        Property Qty As Integer
        Property Price As DTOAmt
        Property Devengo As DTOAmt
        Property Deduccio As DTOAmt

        Public Sub New(ByVal oConcepte As DTONomina.Concepte)
            MyBase.New()
            _Concepte = oConcepte
        End Sub

        Public ReadOnly Property Import As DTOAmt
            Get
                Dim retval As DTOAmt = Nothing
                If _Price IsNot Nothing Then
                    retval = DTOAmt.Factory(_Qty * _Price.Eur)
                End If
                Return retval
            End Get
        End Property

    End Class

    Public Class Concepte
        Public Property Id As Integer
        Public Property Name As String

        Public Sub New(iId As Integer, Optional sName As String = "")
            MyBase.New()
            _Id = iId
            _Name = sName
        End Sub
    End Class

    Public Class Header
        Property DownloadUrl As String
        Property ThumbnailUrl As String
        Property Fch As Date


        Public Class Collection
            Inherits List(Of DTONomina.Header)

            Public Function Years() As List(Of Integer)
                Dim retval = Me.GroupBy(Function(x) x.Fch.Year).
                    Select(Function(y) y.First).
                    Select(Function(z) z.Fch.Year).
                    OrderByDescending(Function(o) o).
                    ToList()
                Return retval
            End Function

            Shared Function Factory(oNominas As List(Of DTONomina)) As Collection
                Dim retval As New Collection
                For Each oNomina In oNominas
                    Dim item = New Header
                    item.Fch = oNomina.Cca.Fch
                    item.ThumbnailUrl = oNomina.Cca.DocFile.ThumbnailUrl()
                    item.DownloadUrl = oNomina.Cca.DocFile.DownloadUrl()
                    retval.Add(item)
                Next
                Return retval
            End Function

            Public Function Serialized() As String
                Dim serializer As New JavaScriptSerializer()
                Return serializer.Serialize(Me)
            End Function

        End Class
    End Class



End Class





