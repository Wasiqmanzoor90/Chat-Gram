import React, { useContext } from 'react';
import { context } from '../Context/Store';

function Comment() {
  // Access the comment data from context
  const { comment } = useContext(context);

  return (
    <div className="container" style={{ minHeight: '100vh' }}>
      <h1>Comments</h1>
      {/* Check if there are comments */}
      {comment && comment.length > 0 ? (
        comment.map((commentItem, index) => (
          <div key={index} className="comment">
            <p><strong>Name:</strong> {commentItem.name}</p>
            <p><strong>Content:</strong> {commentItem.content}</p>
            <p><strong>Created:</strong> {new Date(commentItem.created).toLocaleString()}</p>
          </div>
        ))
      ) : (
        <p>No comments available.</p>
      )}
    </div>
  );
}

export default Comment;
