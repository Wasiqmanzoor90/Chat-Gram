import React, { useContext } from 'react';
import { context } from '../Context/Store';
import { useNavigate } from 'react-router-dom';

function Home() {
  const { posts } = useContext(context);
  const navigate = useNavigate(); // ✅ VALID hook call

  return (
    <div>
      <h2>Posts</h2>
      {posts.length === 0 ? (
        <p>No posts found.</p>
      ) : (
        posts.map((post, index) => (
          <div className='d-block justify-content-center pb-5' key={index} style={{ borderBottom: '1px solid lightgrey', padding: '10px', marginBottom: '20px' }}>
            <h4>Posted by: {post.name}</h4>
            <br />
            {post.postPicUrl && (
              <div className='mb-3' style={{ display: 'flex', justifyContent: 'center' }}>
                <img
                  src={post.postPicUrl}
                  alt="Post"
                  style={{ width: '700px', maxHeight: '400px', objectFit: 'cover' }}
                />
              </div>
            )}
            <h3>{post.caption}</h3>
            <div className='d-flex gap-2'>
              <div>
                <span class="material-symbols-outlined">
                  favorite
                </span>
              </div>
              <div>
                <span
                style={{cursor:'pointer'}}
               onClick={()=> navigate('/comment')}
                class="material-symbols-outlined">
                  mode_comment
                </span>
              </div>
            </div>

            <p>Tags: {post.tags.join(', ')}</p>
            <small >Created: {new Date(post.createdAt).toLocaleString()}</small>
          </div>

        ))
      )}
    </div>
  );
}

export default Home;
