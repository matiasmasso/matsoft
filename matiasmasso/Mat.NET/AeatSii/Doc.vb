Public Class Doc

    Public Class Linea
        Property value As String
        Property row As Integer
        Property offset As Decimal
        Property horizontalAlignment As HorizontalAlignments
        Property font As Fonts
        Property style As styles

        Public Enum Fonts
            standard
            h1
            h2
            h3
            caption
        End Enum

        Public Enum styles
            standard
            bold
            italic
        End Enum

        Public Enum HorizontalAlignments
            left
            right
            center
        End Enum
    End Class

    Public Class Table

    End Class

    Public Class Row

    End Class

    Public Class Cell

    End Class
End Class
