using backoffice.Models;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace frontoffice.Database;

public class PdfService
{
    public MemoryStream ExportToPdf(List<Movie> movies)
    {
        // Create a new PDF document
        PdfDocument document = new PdfDocument();
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);
        XFont font = new XFont("Verdana", 12, XFontStyleEx.Regular);

        // Calculate column widths based on the longest content in each column
        double tableLeftMargin = 40;
        double[] columnWidths = CalculateColumnWidths(movies, font, gfx, tableLeftMargin);

        // Draw table header
        double yPosition = 40; // Initial y-position for the table header
        DrawTableHeader(gfx, font, columnWidths, ref yPosition, tableLeftMargin);

        // Draw table rows
        foreach (var movie in movies)
        {
            DrawTableRow(gfx, font, movie, columnWidths, ref yPosition, tableLeftMargin);
        }

        // Save the document to a memory stream and return as a file download
        MemoryStream stream = new MemoryStream();
        document.Save(stream, false);
        stream.Position = 0;

        return stream;
    }

    private double[] CalculateColumnWidths(List<Movie> movies, XFont font, XGraphics gfx, double tableLeftMargin)
    {
        // Define table columns
        double[] columnWidths = new double[4]; // Assuming there are 4 columns
        const double cellPadding = 10; // Padding for cells

        // Iterate through each movie to find the maximum width for each column
        for (int i = 0; i < columnWidths.Length; i++)
        {
            double maxWidth = 0;

            foreach (var movie in movies)
            {
                double cellWidth = gfx.MeasureString(GetCellValue(movie, i), font).Width + cellPadding;
                if (cellWidth > maxWidth)
                {
                    maxWidth = cellWidth;
                }
            }

            // Set the column width to the maximum width found
            columnWidths[i] = maxWidth;
        }

        return columnWidths;
    }

    private void DrawTableHeader(XGraphics gfx, XFont font, double[] columnWidths, ref double yPosition,
        double tableLeftMargin)
    {
        gfx.DrawString("List of Movies", font, XBrushes.Black, new XPoint(tableLeftMargin, yPosition));
        yPosition += font.Height + 10; // Move down for the header line
        gfx.DrawLine(XPens.Black, tableLeftMargin, yPosition, tableLeftMargin + columnWidths.Sum(), yPosition);
        yPosition += 10; // Move down for the content
    }

    private void DrawTableRow(XGraphics gfx, XFont font, Movie movie, double[] columnWidths, ref double yPosition,
        double tableLeftMargin)
    {
        double currentYPosition = yPosition;

        // Draw cells
        for (int i = 0; i < columnWidths.Length; i++)
        {
            gfx.DrawString(GetCellValue(movie, i), font, XBrushes.Black,
                new XRect(tableLeftMargin, currentYPosition, columnWidths[i], font.Height), XStringFormats.TopLeft);
            tableLeftMargin += columnWidths[i];
        }

        yPosition += font.Height + 5; // Move down for the next row
    }

    private string GetCellValue(Movie movie, int columnIndex)
    {
        switch (columnIndex)
        {
            case 0: return movie.Title;
            case 1: return movie.Duration.ToString();
            case 2: return movie.Category;
            default: return string.Empty;
        }
    }
}