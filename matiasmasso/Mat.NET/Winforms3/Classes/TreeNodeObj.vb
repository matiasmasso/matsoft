Public Class TreeNodeObj
    Inherits System.Windows.Forms.TreeNode

    Private mObj As Object = Nothing
    Private mCod As Integer = 0
    Private mGuid As Guid = Guid.NewGuid

    Public Sub New(Optional ByVal sText As String = "", Optional ByVal oObj As Object = Nothing, Optional ByVal iCod As Integer = 0, Optional ByVal iImageIndex As Integer = -1, Optional ByVal iSelectedImageIndex As Integer = -1)
        MyBase.New(sText, iImageIndex, iSelectedImageIndex)
        MyBase.Text = sText
        mObj = oObj
        mCod = iCod
    End Sub


    Public Property Obj() As Object
        Get
            Return mObj
        End Get
        Set(ByVal Value As Object)
            mObj = Value
        End Set
    End Property

    Public Property Cod() As Integer
        Get
            Return mCod
        End Get
        Set(ByVal value As Integer)
            mCod = value
        End Set
    End Property

    Public Property Children As List(Of TreeNodeObj)
        Get
            Dim retval As New List(Of TreeNodeObj)
            For Each oNode As TreeNodeObj In MyBase.Nodes
                retval.Add(oNode)
            Next
            Return retval
        End Get
        Set(ByVal value As List(Of TreeNodeObj))
            MyBase.Nodes.Clear()
            For Each oNode As TreeNodeObj In value
                MyBase.Nodes.Add(oNode)
            Next
        End Set
    End Property
End Class
