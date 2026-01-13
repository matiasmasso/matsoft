Imports System.Data.SqlClient

Public Class SortedCod
    Private mGuid As System.Guid = System.Guid.Empty
    Private mCategoria As Categorias = Categorias.NotSet
    Private mOrd As String = ""
    Private mNom As String = ""
    Private mExists As Boolean = False

    Public Enum Categorias
        NotSet
        CategoriasProfesionals
    End Enum

    Public Sub New(ByVal oGuid As System.Guid)
        MyBase.New()
        mGuid = oGuid
    End Sub

    Public Sub New(ByVal oCategoria As Categorias, ByVal sNom As String, Optional ByVal sOrd As String = "")
        MyBase.New()
        mCategoria = oCategoria
        mNom = sNom
        mOrd = sOrd
    End Sub

    Public ReadOnly Property Guid() As System.Guid
        Get
            Return mGuid
        End Get
    End Property

    Public Property Categoria() As Categorias
        Get
            If mCategoria = Categorias.NotSet Then SetItm()
            Return mCategoria
        End Get
        Set(ByVal value As Categorias)
            mCategoria = value
        End Set
    End Property

    Public Property Nom() As String
        Get
            If mNom = "" Then SetItm()
            Return mNom
        End Get
        Set(ByVal value As String)
            mNom = value
        End Set
    End Property

    Public Property Ord() As String
        Get
            If mOrd = "" Then SetItm()
            Return mOrd
        End Get
        Set(ByVal value As String)
            mOrd = value
        End Set
    End Property

    Public ReadOnly Property Exists() As Boolean
        Get
            If Not mExists Then SetItm()
            Return mExists
        End Get
    End Property

    Private Sub SetItm()
        Static BlDone As Boolean
        If BlDone Then Exit Sub
        BlDone = True

        If mGuid.Equals(System.Guid.Empty) Then Exit Sub

        Dim SQL As String = "SELECT * FROM SORTEDCODS WHERE GUID LIKE @GUID"
        Dim oDrd As SqlDataReader = Dal.SQLHelper.GetDataReader(SQL, "@GUID", mGuid.ToString)
        If oDrd.Read Then
            mCategoria = CType(oDrd("Categoria"), Categorias)
            mOrd = oDrd("Ord").ToString
            mNom = oDrd("Nom").ToString
            mExists = True
        End If
        oDrd.Close()
    End Sub

    Public Sub Update()
        Dim SQL As String = ""
        Dim oConn As SqlConnection = Nothing

        Try
            SQL = "SELECT * FROM SORTEDCODS WHERE GUID LIKE @GUID"
            oConn = DAL.SQLHelper.SQLConnection()
            Dim oDA As SqlDataAdapter = DAL.SQLHelper.GetSQLDataAdapter(SQL, oConn, "@GUID", mGuid.ToString)
            Dim oDs As New DataSet
            oDA.Fill(oDS)
            Dim oTb As DataTable = oDS.Tables(0)
            Dim oRow As DataRow
            If oTb.Rows.Count = 0 Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                If mGuid.Equals(System.Guid.Empty) Then mGuid = Guid.NewGuid
                oRow("GUID") = mGuid.ToString
            Else
                oRow = oTb.Rows(0)
            End If

            oRow("ORD") = mOrd
            oRow("NOM") = mNom
            oDA.Update(oDS)

            mExists = True

        Catch e As SqlException
            BLL.MailHelper.MailErr(e.Message & vbCrLf & SQL)

        Finally
            If oConn IsNot Nothing Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
End Class
