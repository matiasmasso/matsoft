Public Class Doc
    Property Sections As List(Of Section)

    Public Function addSection(cod As Section.cods) As Section
        Dim retval As New Section
        With retval
            .cod = cod
            .Lines = New List(Of Line)
        End With
        Return retval
    End Function

    Public Class Section
        Property cod As cods
        Property Lines As List(Of Line)
        Public Enum cods
            header
            header2ndPages
            body
        End Enum

        Public Function addLine(Optional value As String = "", Optional row As Integer = -1, Optional offset As Decimal = 0, Optional alignment As HorizontalAlignments = HorizontalAlignments.left, Optional font As Fonts = Fonts.standard, Optional style As styles = styles.standard) As Line
            Dim retval As New Line
            With retval
                .value = value
                .row = row
                .offset = offset
                .alignment = alignment
                .font = font
                .style = style
            End With
            Return retval
        End Function
    End Class

    Public Class Line
        Property value As String
        Property row As Integer
        Property offset As Decimal
        Property alignment As HorizontalAlignments
        Property font As Fonts
        Property style As styles




    End Class

    Public Class Table
        Property columns As List(Of Column)

    End Class

    Public Class Column
        Property caption As String
        Property width As Decimal
        Property horizontalAlignment As HorizontalAlignments
    End Class

    Public Class Row

    End Class

    Public Class Cell

    End Class

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
