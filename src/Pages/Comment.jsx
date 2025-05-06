import React, { useContext, useEffect, useState, useRef } from 'react';
import { useParams } from 'react-router-dom';
import { context } from '../Context/Store';

function Comment() {
  const { comment, GetComment, CreateCommment } = useContext(context);

  // Get the PostId from the URL parameters
  const { postId } = useParams();

  // Local state for new comment input
  const [newComment, setNewComment] = useState('');

  // Ref to store fetched postIds to avoid re-fetching
  const fetchedPostsRef = useRef(new Set());

  useEffect(() => {
    // Fetch comments only if they haven't already been fetched
    if (postId && !fetchedPostsRef.current.has(postId)) {
      GetComment(postId);
      fetchedPostsRef.current.add(postId); // Mark this postId as fetched
    }
  }, [postId, GetComment]);

  // Handle submitting a new comment
  const handleCommentSubmit = async (e) => {
    e.preventDefault();

    if (newComment.trim() === '') {
      alert('Please enter a comment.');
      return;
    }

    // Create a new comment for the specific post
    await CreateCommment(postId, newComment);

    // Optionally re-fetch comments or update locally (you may already push it in context)
    await GetComment(postId);

    // Clear the input after submitting the comment
    setNewComment('');
  };

  return (
    <div className="container" style={{ minHeight: '100vh' }}>
      <h1 className="mb-4">Comments</h1>

      {/* Comment list */}
      {comment && comment.length > 0 ? (
        comment.map((commentItem, index) => (
          <div key={index} className="comment">
            <b style={{ paddingRight: '7px' }}>{commentItem.name}:</b> {commentItem.content}
            <p style={{ fontSize: '0.7rem' }}>{new Date(commentItem.created).toLocaleString()}</p>
            <span
              style={{ display: 'block', width: '100%', borderBottom: '1px solid lightgrey' }}
            ></span>
          </div>
        ))
      ) : (
        <p>No comments available.</p>
      )}

      {/* Comment input form */}
      <div className="d-flex justify-content-center" style={{ marginTop: '70vh', marginBottom: '4px' }}>
        <input
          style={{ borderRadius: '8px', border: '1px solid lightgrey' }}
          className="w-100"
          type="text"
          placeholder="Add a comment..."
          value={newComment}
          onChange={(e) => setNewComment(e.target.value)}
        />
        <button className="btn btn-primary ms-1" onClick={handleCommentSubmit}>
          Post
        </button>
      </div>
    </div>
  );
}

export default Comment;
