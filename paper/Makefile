PDF=paper.pdf
TEX=paper.tex

.PHONY: all clean

all: $(PDF)

$(PDF): $(TEX) references.bib
	pdflatex -interaction=nonstopmode $(TEX)
	bibtex paper
	pdflatex -interaction=nonstopmode $(TEX)
	pdflatex -interaction=nonstopmode $(TEX)

clean:
	rm -f paper.aux paper.bbl paper.blg paper.log paper.out $(PDF)
