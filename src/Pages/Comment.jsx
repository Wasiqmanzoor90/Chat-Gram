import React, { useContext } from 'react';
import { context } from '../Context/Store';

function Comment() {
  // Access the comment data from context
  const { comment } = useContext(context);

  return (
    <div className="container" style={{ minHeight: '100vh'}}>
      <h1 className='mb-4'>Comments</h1>
      {/* Check if there are comments */}
      {comment && comment.length > 0 ? (
        comment.map((commentItem, index) => (
          <div key={index} className="comment">
            <b style={{paddingRight:'7px'}}>{commentItem.name}:</b> {commentItem.content}
            <p style={{fontSize:'0.7rem'}}> {new Date(commentItem.created).toLocaleString()}</p>
            <span style={{ display: 'block', width: '100%', borderBottom: '1px solid lightgrey' }}></span>

          </div>
        ))
      ) : (
        <p>No comments available.</p>
      )}
      <div className='d-flex justify-content-center' style={{marginTop:'70vh', marginBottom:'4px'}}>
        <input style={{borderRadius:'8px' , border:'1px solid lightgrey'}} className='w-100' type="text" placeholder='Add comments...' />
        <button className='btn btn-primary ms-1'>Post</button>
      </div>
    </div>

    
  );
}

export default Comment;
