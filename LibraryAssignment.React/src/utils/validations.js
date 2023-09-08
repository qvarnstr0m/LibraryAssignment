export const validateBook = (book) => {
    console.log(book)
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
    if (book == null) {
        errors.book = 'Book is required';
    }
    return errors;
}