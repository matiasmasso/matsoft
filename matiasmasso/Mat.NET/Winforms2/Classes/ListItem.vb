Public Class ListItem
    Private mValue As Integer
    Private mText As String
    Property Tag As DTOBaseGuid

    Public Sub New(Optional ByVal iValue As Int16 = 0, Optional ByVal sText As String = "")
        mValue = iValue
        mText = sText
    End Sub

    Public Property Value() As Integer
        Get
            Return mValue
        End Get
        Set(ByVal iValue As Integer)
            mValue = ivalue
        End Set
    End Property

    Public Property Text() As String
        Get
            Return mText
        End Get
        Set(ByVal value As String)
            mText = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return mText
    End Function
End Class
