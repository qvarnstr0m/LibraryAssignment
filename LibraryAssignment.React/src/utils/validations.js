export const validateBook = (book) => {
    const errors = {};
    if (!book.title) {
        errors.title = 'Title is required';
    }
    if (!book.author) {
        errors.author = 'Author is required';
    }
    if (!book.description) {
        errors.description = 'Description is required';
    }
    if (!book.isbn) {
        errors.isbn = 'Isbn is required';
    }
    if (!book) {
        errors.book = 'Book is required';
    }
    return errors;
}