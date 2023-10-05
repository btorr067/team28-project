# alt-f4-team-project
This is the repository for the Team28 - ALT-F4 online book store restful api project.

## Features with assignment
### Book Details 

Users can see informative and enticing details about a book
This feature will be handled by Brandon T.

REST API Actions:
  * An Administrator must be able to create a book with the book ISBN, book name, book description, price, author, publisher, year published, copies sold.
  * Must be able to retrieve a book's details by the ISBN.
  * An administrator must be able to create an author with first name, last name, biography and publisher.
  * Must be able to retrieve a list of books associated with an author.

### Profile Management 

Users can create and maintain their profiles rather than enter in their information each time they order.
This feature will be handled by Nicolas T.

REST API Actions:
  * Create a User with username, password and optional fields (name, email address, home address)
  * Retrieve a User Object and its fields by their username
  * Update the user and any of their fields except for mail
  * Create Credit Card that belongs to a User

### Shopping Cart  

Users can manage items in a shopping cart for immediate or future Purchase.
This feature will be handled by Josue T.

REST API Actions:
  * Retrieve the subtotal price of all items in the user’s shopping cart.
  * Add a book to the shopping cart.
  * Retrieve the list of book(s) in the user’s shopping cart.
  * Delete a book from the shopping cart instance for that user.

### Wish List Management 

Users can create and have 3 different wish lists which can have books moved to from the primary list.
This feature will be handled by Asifa T.

REST API Actions:
  * Must be able to create a wishlist of books that belongs to user and has a unique name.
  * Must be able to add a book to a user’s wishlisht.
  * Must be able to remove a book from a user’s wishlist into the user’s shopping cart.
  * Must be able to list the book’s in a user’s wishlist.


